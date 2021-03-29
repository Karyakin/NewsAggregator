using AutoMapper;
using Contracts.RepositoryInterfaces;
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
        private readonly IRepositoryWrapper _wrapper;
        private readonly IMapper _mapper;
        public NewsController(IRepositoryWrapper wrapper, IMapper mapper)
        {
            _wrapper = wrapper;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
           // var companies = await _wrapper.News.FindAll(trackChanges: false).ToListAsync();
            var companies = await _wrapper.News.GetAllNewsAsync(trackChanges: false);

            var getCompanyDTO = _mapper.Map<IEnumerable<NewsGetDTO>>(companies).ToList();
           // var getCompanyDTO = _mapper.Map<IEnumerable<NewsCategoryRssSourceDTO>>(companies).ToList();



            return Ok(getCompanyDTO);
        }

        [HttpPut("AddNews")]
        public async Task<IActionResult> AddNews(string categoryName, string rssSourceName, News news)
        {
            categoryName = "Искуство";
            rssSourceName = "TutBy";

          /*  var catgory = await _wrapper.Category.FindCategoryByName(categoryName);
            var rssSource = await _wrapper.RssSource.FindRssSourceByName(rssSourceName);*/

            News news1 = new News()
            {
                CategoryId =/* catgory.Id,*/(await _wrapper.Category.FindCategoryByName(categoryName)).Id,
                SourceId = /*rssSource.Id,*/(await _wrapper.RssSource.FindRssSourceByName(rssSourceName)).Id,
                Content = "Описывается сама новость и что произошло",
                Rating = 2,
                Title = "А вы знали что...",
                Url = "https://news.tut.by/society/724224.html"
            };


           

            _wrapper.News.Create(news1);
            await _wrapper.SaveAsync();
            return Ok();
        }
    }
}
