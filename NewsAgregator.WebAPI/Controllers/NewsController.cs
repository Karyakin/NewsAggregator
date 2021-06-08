using Contracts.ServicesInterfacaces;
using Entities.Entity.NewsEnt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAgregator.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(Guid id)
        {
            var news = await _newsService.GetNewsBiId(id);

            if (news is null)
            {
                Log.Error("News not found");
                return BadRequest("News not found");
            }

            return Ok(news);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var news = await _newsService.FindAllNews();
            if (news is null)
            {
                Log.Error("News not found");
                return BadRequest("News not found");
            }

            return Ok(news);
        }

      /*
        [HttpPost]
        public async Task<IActionResult> Updete()
        {
           
            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> UpdeteEnt()
        {

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Delete()
        {

            return Ok();
        }*/

    }
}
