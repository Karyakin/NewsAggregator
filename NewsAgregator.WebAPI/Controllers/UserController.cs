using Contracts.ServicesInterfacaces;
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
    public class UserController : ControllerBase
    {
       /* private readonly IUserService _userService;
        public UserController(IUserService userService )
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(Guid? id)
         {
            if (!id.HasValue)
            {
                Log.Warning("required parametr was not received");
                return BadRequest("required parametr was not received");
            }
            var user = await _userService.GetUserById(id.Value);

            if (user is null)
            {
                Log.Error("user not found");
                return BadRequest("user not found");
            }

            return Ok(user);
        }

       [HttpGet]
        public async Task<IActionResult> GetOne(string login, string name)
        {
            var users = await _userService.GetAllUsersWithPhoneROleMail();

            if (!string.IsNullOrEmpty(login))
            {
                users = users.Where(x => x.Login.Contains(login));
            }

            if (!string.IsNullOrEmpty(name))
            {
                users = users.Where(x => x.LastName.Contains(name));
            }

            return Ok(users);
        }*/

       
    }
}
