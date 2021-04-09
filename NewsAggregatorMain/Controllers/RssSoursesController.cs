using AutoMapper;
using Contracts.ServicesInterfacaces;
using Contracts.UnitOfWorkInterface;
using Entities.Entity.NewsEnt;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAggregatorMain.Controllers
{
    public class RssSoursesController : Controller
    {
        /*private readonly IRepositoryWrapper _wrapper;
        private readonly IMapper _mapper;*/

        private readonly IRssSourceService _rssSourceService;
        public RssSoursesController(IRssSourceService rssSourceService)
        {
            _rssSourceService = rssSourceService;
        }

        public async Task<IActionResult> Index()
        {
            
            var rssSource = await _rssSourceService.GetAllRssSourceAsync(false);


            return View(rssSource);

        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sourse = await _rssSourceService.RssSourceById(id);
          

            if (sourse == null)
            {
                return NotFound();
            }

            return View(sourse);
        }


        public async Task<IActionResult> DetailsWithNews(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sourse = await _rssSourceService.RssSourceByIdWithNews(id);


            if (sourse == null)
            {
                return NotFound();
            }

            return View(sourse);
        }




        public IActionResult Create(RssSource rssSource)
        {

            return  View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RssSourceModel rssSourceModel)
        {
            

            if (ModelState.IsValid)
            {
               // rssSourceModel.Id = Guid.NewGuid();
                await _rssSourceService.CreateOneRssSource(rssSourceModel);

            }
          /*  RssSource SportExpress = new RssSource()
            {
                Id = Guid.NewGuid(),
                Name = "Спорт-экспресс",
                Link = "https://www.sport-express.ru/services/materials/news/se/",
               // DateOfReceiving = DateTime.Now;
            };*/

          // await _rssSourceService.CreateOneRssSource(rssSourceModel);

          /*  _wrapper.RssSource.CreateOneRssSource(TutBy);
            await _wrapper.SaveAsync();*/

            return  RedirectToAction(nameof(Index));
        }
    }
}
