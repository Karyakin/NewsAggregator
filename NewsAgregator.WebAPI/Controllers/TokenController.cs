using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using NewsAgregator.WebAPI.Auth;
using Contracts.ServicesInterfacaces;

namespace NewsAgregator.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TokenController : ControllerBase
    {
        private readonly IJwtAuthManager _jwtAuthManager;

        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public TokenController(IJwtAuthManager jwtAuthManager, IUserService userService, IRoleService roleService)
        {
            _jwtAuthManager = jwtAuthManager;
            _userService = userService;
            _roleService = roleService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            //todo получить роль нашего пользователя и 

            var user = await _userService.GetUserByLogin(request.Login);
            var userRole = await _roleService.GetRoleIdyById(user.RoleId);

            //todo написать метод логинации чтобы сверять логин и пароль, и если все норм выдаем токен

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
