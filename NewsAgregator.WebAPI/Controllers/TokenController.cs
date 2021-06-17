using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using NewsAgregator.WebAPI.Auth;

namespace NewsAgregator.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TokenController : ControllerBase
    {
        private readonly IJwtAuthManager _jwtAuthManager;

        public TokenController(IJwtAuthManager jwtAuthManager)
        {
            _jwtAuthManager = jwtAuthManager;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            //todo получить роль нашего пользователя и 

            JwtAuthResult jwtResult;
            if (request.Login == "Agent")// 
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, request.Login),
                    new Claim(ClaimTypes.Role, "Admin")
                };

                jwtResult = _jwtAuthManager.GenerateTokens(request.Login, claims);
            }
            else
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, request.Login),
                    new Claim(ClaimTypes.Role, "User")
                };
                jwtResult = _jwtAuthManager.GenerateTokens(request.Login, claims);
            }
            return Ok(jwtResult);
        }
    }
}
