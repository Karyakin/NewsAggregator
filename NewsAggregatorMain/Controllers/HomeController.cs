using AutoMapper;
using Contracts.UnitOfWorkInterface;
using Entities.DataTransferObject;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsAggregatorMain.Filters;
using NewsAggregatorMain.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAggregatorMain.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var today = DateTime.Today;

            var bestNewsEnt = await _unitOfWork.News.GetAll(false)
                .Where(t => t.StartDate.Date == today)
                .OrderByDescending(x => x.Rating)
                .Select(x=>_mapper.Map<NewsGetDTO>(x))
                .FirstOrDefaultAsync();

            var badNewsEnt = await _unitOfWork.News.GetAll(false)
                .Where(t => t.StartDate.Date == today)
                .OrderBy(x => x.Rating)
                .Select(x => _mapper.Map<NewsGetDTO>(x))
                .FirstOrDefaultAsync();

            var news = new NewsForHomePageModel
            {
                GoogNews = bestNewsEnt,
                BadNews = badNewsEnt
            };

            return View(news);
        }

        [ChromFilterAttribute]
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// В данном случаем экшин фильтр CheckDataFilterAttribute под капотом получает список новостей и передает их в метод
        /// </summary>
        /// <param name="hiddenId">параметр задается при поможи атрибуда. Такми образом неявно можно подкидывать в запрос любую информацию</param>
        /// <param name="news">список новостей, которые получает атрибут фильтр</param>
        /// <returns></returns>
        [ServiceFilter(typeof(CheckDataFilterAttribute))]
        public IActionResult Privacy1(int hiddenId, IEnumerable<NewsGetDTO> news)
        {
            return View(news);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
