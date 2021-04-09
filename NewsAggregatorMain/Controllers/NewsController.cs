using AutoMapper;
using Contracts.RepositoryInterfaces;
using Contracts.ServicesInterfacaces;
using Contracts.UnitOfWorkInterface;
using Entities.DataTransferObject;
using Entities.Entity.NewsEnt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAggregatorMain.Controllers
{
    //[Route("[controller]")]
    public class NewsController : Controller
    {
      
        private readonly IUnitOfWork _unitOfWork;
        public NewsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var allCompanies = await _unitOfWork.News.GetAll(false).ToListAsync();
            #region Через ркпу
            /*
                       // var companies = await _wrapper.News.FindAll(trackChanges: false).ToListAsync();
                        var companies = await _wrapper.News.GetAllNewsAsync(trackChanges: false);

                        var getCompanyDTO = _mapper.Map<IEnumerable<NewsGetDTO>>(companies).ToList();
                       // var getCompanyDTO = _mapper.Map<IEnumerable<NewsCategoryRssSourceDTO>>(companies).ToList()*/

            #endregion
            return View(allCompanies);
        }



        [HttpPost]
        public async Task<IActionResult> Create(string categoryName, string rssSourceName, News news)
        {
            categoryName = "Искуство";
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

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}
