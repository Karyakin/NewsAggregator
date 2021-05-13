using Entities.DataTransferObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewsAggregatorMain.Filters;
using NewsAggregatorMain.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAggregatorMain.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
          /*  try
            {
                throw new Exception("test");
            }
            catch (Exception e)
            {
                Log.Fatal($"все, кабзда работе{e}");
               // throw; // throw выпускает исключение наружу
            }*/

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
