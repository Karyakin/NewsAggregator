using AutoMapper;
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
        public async Task <IActionResult> Index()
        {
            var companies = await _wrapper.News.FindAll(trackChanges: false).ToListAsync();

            var getCompanyDTO = _mapper.Map<IEnumerable<NewsGetDTO>>(companies);

            return Ok(getCompanyDTO);
        }

        [HttpGet("AddNews")]
        public async Task <IActionResult> AddNews(News news)
        {
             _wrapper.News.Create(news);
            await _wrapper.SaveAsync();
            return Ok();
        }
    }
}
