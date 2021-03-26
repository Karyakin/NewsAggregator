using AutoMapper;
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
        private readonly IRepositoryWrapper _wrapper;
        private readonly IMapper _mapper;

        public RssSourceController(IRepositoryWrapper wrapper, IMapper mapper)
        {
            _wrapper = wrapper;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            
            var rssSource = await _wrapper.RssSource.GetAllRssSourceAsync(false);

            return Ok(rssSource);
        }

        public async Task<IActionResult> AddOneRssSource()
        {
            RssSource TutBy = new RssSource()
            {
                Id = Guid.NewGuid(),
                Name = "TutBy",
                Link = "https://news.tut.by/rss/all.rss",
               // DateOfReceiving = DateTime.Now;
            };

            _wrapper.RssSource.CreateOneRssSource(TutBy);
            await _wrapper.SaveAsync();

            return  Ok();
        }
    }
}
