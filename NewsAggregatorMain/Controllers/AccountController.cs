using Contracts.ServicesInterfacaces;
using Microsoft.AspNetCore.Mvc;
using NewsAggregatorMain.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAggregatorMain.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]

        [ValidateAntiForgeryToken]// страница неявно внутри себя сгенерирует разметку, эта разметак будет передавать с собой соответсвующий токен где на BE он будет проверятся и если токен не сошелся, значет он пришел не со страницы, а откуда-то еще. т.е. если запрос пришел не сайта, то его обробатывать не нужно
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var passwordHash = _userService.GetPasswordHash(model.Password);

                /*                var result = await _userService.RegisterUser(new UserDto()
                                {
                                    Id = Guid.NewGuid(),
                                    Email = model.Email,
                                    PasswordHash = passwordHash
                                });

                                if (result)
                                {
                                    return RedirectToAction("Index", "Home");
                                }
                                return BadRequest(model);*/
            }
            return View(model);
        }

    }
}
