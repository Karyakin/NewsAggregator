﻿using Contracts.ServicesInterfacaces;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Threading.Tasks;

namespace NewsAgregator.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
            try
            {
                var sources = await _rssSourceService.RssSourceByNameAndUrl(name, url);
                return Ok(sources);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return BadRequest(ex.Message);
                throw;
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var sources = await _rssSourceService.GetAllRssSourceAsync(false);
                return Ok(sources);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return BadRequest(ex.Message);
                throw;
            }
        }


        [HttpPost]
        public async Task<IActionResult> Post(RssSourceModel rssSourceModel)
        {
            try
            {
                await _rssSourceService.CreateOneRssSource(rssSourceModel);
                return Ok($"SrrSource \"{rssSourceModel.Name}\" was successfully created");
            }
            catch (Exception ex)
            {
                BadRequest(ex.Message);
                throw;
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Post(Guid id)
        {
            var rssSourseModel = await _rssSourceService.GetRssSourceById(id);

            if (rssSourseModel is null)
            {
                Log.Error($"Can't find rss sourse by id - {id}");
                return BadRequest($"Can't find rss sourse by id - {id}");
            }

            try
            {
                var res = await _rssSourceService.DeleteRssSourse(id);//можно пернуть количество удаленных записей
                return Ok($"Rss Sourse \"{rssSourseModel.Name}\" was successfully deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }
    }
}
