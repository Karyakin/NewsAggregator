﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NewsAgregator.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TestController : ControllerBase
    {
        [HttpGet("getdefault")]
        public IActionResult GetDefault()
        {
            return Ok("Authorized User");
        }

        [HttpGet("getAdmin")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAdmin()
        {
            return Ok("Admin User");
        }

        [HttpGet("GetUnauthorized")]
        [AllowAnonymous]
        public IActionResult GetUnauthorized()
        {
            return Ok("unauthorized User");
        }
    }
}
