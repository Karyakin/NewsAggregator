using Contracts.ServicesInterfacaces;
using Entities.DataTransferObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewsAggregatorMain.Filters;
using NewsAggregatorMain.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace NewsAggregatorMain.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View();
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
