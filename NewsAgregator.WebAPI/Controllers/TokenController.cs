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
using System.Security.Cryptography;
using System.Text;

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
            var user = await _userService.GetUserByLogin(request.Login);
            var userRole = await _roleService.GetRoleIdyById(user.RoleId);

            if (user == null)
                return BadRequest("Пользователь с таким именем не зарегистрирован!");
            using (var hmac = new HMACSHA512(user.PasswordSalt))
            {
                var computesHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
                for (int i = 0; i < computesHash.Length; i++)
                {
                    if (computesHash[i] != user.PasswordHash[i])
                        return Unauthorized("Пользователь с таким именем не зарегистрирован!");//ошибка авторизации. не смог авторизоваться
                }
            }

            JwtAuthResult jwtResult;

            var claims = new[]
            {
                    new Claim(ClaimTypes.Name, request.Login),
                    new Claim(ClaimTypes.Role, userRole.Name)
            };

            jwtResult = _jwtAuthManager.GenerateTokens(request.Login, claims);
            return Ok(jwtResult);
        }
    }
}
