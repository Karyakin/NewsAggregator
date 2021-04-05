using AutoMapper;
using Contracts.RepositoryInterfaces;
using Contracts.ServicesInterfacaces;
using Contracts.WrapperInterface;
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
        private readonly INewsService _newsService;
        private readonly ICategoryService _categoryService;
        private readonly IRssSourceService _rssSourceService;
        public NewsController(INewsService newsService, ICategoryService categoryService, IRssSourceService rssSourceService)
        {
            _newsService = newsService;
            _categoryService = categoryService;
            _rssSourceService = rssSourceService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var allCompanies = await _newsService.FindAllNews();
            #region Через ркпу
            /*
                       // var companies = await _wrapper.News.FindAll(trackChanges: false).ToListAsync();
                        var companies = await _wrapper.News.GetAllNewsAsync(trackChanges: false);

                        var getCompanyDTO = _mapper.Map<IEnumerable<NewsGetDTO>>(companies).ToList();
                       // var getCompanyDTO = _mapper.Map<IEnumerable<NewsCategoryRssSourceDTO>>(companies).ToList()*/

            #endregion
            return Ok(allCompanies);
        }

        [HttpGet("oneNews")]
        public async Task<IActionResult> GetOneNews()
        {
            var guid = (await _newsService.FindAllNews()).FirstOrDefault();// это метод на удаление потом, он чисто для получения гуида
            var test = guid.Id;
            
           var res = await _newsService.GetNewsBiId(test);

            return Ok(res);
        }



        [HttpPut("AddNews")]
        public async Task<IActionResult> AddNews(string categoryName, string rssSourceName, News news)
        {
            categoryName = "Искуство";
            rssSourceName = "TutBy";

            News news1 = new News()
            {
                CategoryId = (await _categoryService.FindCategoryByName(categoryName)).Id,
                SourceId = (await _rssSourceService.RssSourceByName(rssSourceName)).Id,
                Content = "Описывается сама новость и что произошло",
                Rating = 2,
                Title = "А вы знали что...",
                Url = "https://news.tut.by/society/724224.html"
            };

            await _newsService.CreateOneNewsAsync(news1);

            return Ok();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}
