using AutoMapper;
using Contracts.ServicesInterfacaces;
using Contracts.UnitOfWorkInterface;
using Entities.Entity.NewsEnt;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;

namespace NewsAggregatorMain.Controllers
{
    [Authorize]
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
                try
                {
                    using (var reader = XmlReader.Create(rssSourceModel.Url))
                    {
                        var feed = SyndicationFeed.Load(reader);
                        reader.Close();
                    }
                    await _rssSourceService.CreateOneRssSource(rssSourceModel);
                }
                catch (Exception ex)
                {
                    Log.Error($"Something went wrong when trying to add to the source. Message: {ex.Message}");
                    return BadRequest( $"Something went wrong when trying to add to the source. Incorrect Rss Sourse adress!");

                }
            }

            return  RedirectToAction(nameof(Index));
        }
    }
}
