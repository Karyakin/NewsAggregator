using AutoMapper;
using Contracts.RepositoryInterfaces;
using Contracts.ServicesInterfacaces;
using Contracts.UnitOfWorkInterface;
using Entities.DataTransferObject;
using Entities.Entity.NewsEnt;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewsAggregatorMain.Models;
using NewsAggregatorMain.Models.ViewModel.NewsVM;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace NewsAggregatorMain.Controllers
{
    //[Route("[controller]")]
    public class NewsController : Controller
    {

        private readonly INewsService _newsService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRssSourceService _rssSourceService;
        public NewsController(INewsService newsService, IUnitOfWork unitOfWork, IRssSourceService rssSourceService)
        {
            _newsService = newsService;
            _unitOfWork = unitOfWork;
            _rssSourceService = rssSourceService;
        }

        public IActionResult Aggregate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Aggregate(CreateNewsViewModel createNewsViewModel)
        {
            var rsssouses = await _rssSourceService.GetAllRssSourceAsync(false);
            var newInfos = new List<NewsInfoFromRssSourseDto>(); // without any duplicate

            foreach (var item in rsssouses)
            {
                /*if (item.Name.Equals("TUT.by"))
                {

                }*/
                    var newsList = await _newsService.GetNewsInfoFromRssSourse(item);
                    newInfos.AddRange(newsList);

            };

            await _newsService.CreateManyNewsAsync(newInfos);
            return RedirectToAction(nameof(Index));
        }




        public async Task<IActionResult> Index(int page = 1)
        {
            var allNews = (await _newsService.FindAllNews()).ToList();

            var pageSize = 9;
            var newsPerPages = allNews.Skip((page - 1) * pageSize).Take(pageSize);
            var pageInfo = new PageInfo()
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = allNews.Count
            };

            return View(new NewsListWithPaginationInfo()
            {
                News = newsPerPages,
                PageInfo = pageInfo
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateNewsViewModel createNewsViewModel)
        {
            var a = await _rssSourceService.GetAllRssSourceAsync(false);//

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new CreateNewsViewModel()
            {
                Sources = new SelectList(await _rssSourceService.GetAllRssSourceAsync(false),
               "Id",
               "Name")
            };



            return View(model);
        }

        public async Task<IActionResult> Details(NewsGetDTO newsGetDTO)
        {
            var newsWithDetails = await _newsService.GetNewsBiId(newsGetDTO.Id);

            return View(newsWithDetails);

        }

        [HttpPost]
        void Delete(News news)
        {
            throw new NotImplementedException();
        }
    }
}
