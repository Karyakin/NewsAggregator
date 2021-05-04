using Contracts.ServicesInterfacaces;
using Contracts.UnitOfWorkInterface;
using Entities.DataTransferObject;
using Entities.Entity.Users;
using Microsoft.AspNetCore.Mvc;
using NewsAggregatorMain.Models.Account;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAggregatorMain.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

                 

        public AccountController(IUserService userService, IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;


        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]// страница неявно внутри себя сгенерирует разметку, эта разметак будет передавать с собой соответсвующий токен где на BE он будет проверятся и если токен не сошелся, значет он пришел не со страницы, а откуда-то еще. т.е. если запрос пришел не сайта, то его обробатывать не нужно
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
             



            if (ModelState.IsValid)
            {
                var hashSoult = _userService.GetPasswordHashSoult(registerDto.Password);

                if (await _userService.UserExist(registerDto.Login))
                {
                    return BadRequest("User allredy exist");
                }



                    var newUser =   await  _userService.ArrangeNewUser(registerDto, hashSoult);

              

                try
                {
                    _unitOfWork.User.Add(newUser);
                    await _unitOfWork.SaveAsync();
                }
                catch (Exception ex)
                {
                    Log.Error($"Can not compleate greate new User. Details: {ex.Message}");
                    throw;
                }
            }
            return View(registerDto);
        }

    }
}
