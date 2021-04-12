using AutoMapper;
using Contracts.RepositoryInterfaces;
using Contracts.ServicesInterfacaces;
using Contracts.UnitOfWorkInterface;
using Entities.DataTransferObject;
using Entities.Entity.NewsEnt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewsAggregatorMain.Models;
using NewsAggregatorMain.Models.ViewModel.NewsVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

       // [HttpGet]
        public async Task<IActionResult> Index(int page=1)
        {
            var allNews =  (await _newsService.FindAllNews()).ToList();


           // int page = 2;

            var pageSize = 4;
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
           // var a = await _rssSourceService.GetAllRssSourceAsync(false);//
          


           



           /* categoryName = "Искуство";
            rssSourceName = "TutBy";

            var aa = await _unitOfWork.Category.GetByCondition(n => n.Name.Equals(categoryName), false).SingleOrDefaultAsync();
            var bb = await _unitOfWork.RssSource.GetByCondition(n=>n.Name.Equals(rssSourceName), false).SingleOrDefaultAsync();

            News news1 = new News()
            {
                CategoryId = aa.Id,
                SourceId = bb.Id,
                Content = "Описывается сама новость и что произошло",
                Rating = 2,
                Title = "А вы знали что...",
                Url = "https://news.tut.by/society/724224.html"
            };

            await _unitOfWork.News.Add(news1);
            await _unitOfWork.SaveAsync();

            return RedirectToAction(nameof(Index));*/
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
    }
}
