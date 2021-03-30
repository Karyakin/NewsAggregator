using AutoMapper;
using Contracts.ServicesInterfacaces;
using Contracts.WrapperInterface;
using Entities.Entity.NewsEnt;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAggregatorMain.Controllers
{
    public class RssSourceController : Controller
    {
        /*private readonly IRepositoryWrapper _wrapper;
        private readonly IMapper _mapper;*/

        private readonly IRssSourceService _rssSourceService;
        public RssSourceController(IRssSourceService rssSourceService)
        {
            _rssSourceService = rssSourceService;
        }

        public async Task<IActionResult> Index()
        {
            
            var rssSource = await _rssSourceService.GetAllRssSourceAsync(false);

            return Ok(rssSource);
        }

        public async Task<IActionResult> AddOneRssSource()
        {
            RssSource SportExpress = new RssSource()
            {
                Id = Guid.NewGuid(),
                Name = "Спорт-экспресс",
                Link = "https://www.sport-express.ru/services/materials/news/se/",
               // DateOfReceiving = DateTime.Now;
            };

           await _rssSourceService.CreateOneRssSource(SportExpress);

          /*  _wrapper.RssSource.CreateOneRssSource(TutBy);
            await _wrapper.SaveAsync();*/

            return  Ok();
        }
    }
}
