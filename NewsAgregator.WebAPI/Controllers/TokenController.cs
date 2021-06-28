using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using NewsAgregator.WebAPI.Auth;
using Contracts.ServicesInterfacaces;
using System.Security.Cryptography;
using System.Text;
using Serilog;

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



        #region Don't read
        /*
               [AllowAnonymous]
               [HttpPost]
               [Route("Refresh")]
               public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
               {
                   if (!await _refreshTokenService.CheckIsRefreshTokenIsValid(request.Token))
                   {
                       return BadRequest("Invalid Refresh Token");
                   }

                   var userEmail = await _userService.GetUserEmailByRefreshToken(request.Token);
                   if (!string.IsNullOrEmpty(userEmail))
                   {
                       var jwtAuthResult = await GetJwt(userEmail);
                       return Ok(jwtAuthResult);
                   }

                   return BadRequest("Email or password is incorrect");
               }

               private async Task<JwtAuthResult> GetJwt(string email)
               {
                   JwtAuthResult jwtResult;
                   var roleName = await _userService.GetUserRoleNameByEmail(email);

                   var claims = new[]
                   {
                       new Claim(ClaimTypes.Email, email),
                       new Claim(ClaimTypes.Role, roleName)
                   };

                   jwtResult = await _jwtAuthManager.GenerateTokens(email, claims);
                   return jwtResult;
               }*/
        #endregion
    }
}
