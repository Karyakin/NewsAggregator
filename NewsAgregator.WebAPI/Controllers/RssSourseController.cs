using Contracts.ServicesInterfacaces;
using Entities.Entity.NewsEnt;
using Entities.Models;
using MediatR;
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
    public class RssSourseController : ControllerBase
    {
        private readonly IRssSourceService _rssSourceService;

        public RssSourseController(IRssSourceService rssSourceService)
        {
            _rssSourceService = rssSourceService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var rssSourse = await _rssSourceService.GetRssSourceById(id);

            if (rssSourse is null)
            {
                Log.Error("user not found");
                return BadRequest("user not found");
            }

            return Ok(rssSourse);
        }

        [HttpGet]
        public async Task<IActionResult> Get(string name, string url)
        {
            var sources = await _rssSourceService.RssSourceByNameAndUrl(name, url);

            /* if (!string.IsNullOrEmpty(name))
             {
                 sources = sources.Where(dto => dto.Name.Contains(name));
             }
             if (!string.IsNullOrEmpty(url))
             {
                 sources = sources.Where(dto => dto.Url.Contains(url));
             }
             //*/
            return Ok(sources);
        }


        [HttpPost]
        public async Task<IActionResult> Post(RssSourceModel rssSourceModel)
        {
           /* var rssSourseModel = new RssSourceModel();*/
            await _rssSourceService.CreateOneRssSource(rssSourceModel);

            return Ok();
        }

        /*   [HttpGet("empcomp/{companyId}")]*/
        [HttpDelete("{id}")]
        public async Task<IActionResult> Post(Guid id)
        {
            var rssSourseModel = await _rssSourceService.GetRssSourceById(id);
             
            if (rssSourseModel is null)
            {
                Log.Error($"Can't find rss sourse by id - {id}");
                return BadRequest($"Can't find rss sourse by id - {id}");
            }

            var res = await _rssSourceService.DeleteRssSourse(id);

            return Ok($"Rss Sourse \"{rssSourseModel.Name}\" was successfully deleted");
        }
    }

}
