using Contracts.ServicesInterfacaces;
using Contracts.UnitOfWorkInterface;
using Entities.DataTransferObject;
using Entities.Entity.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private readonly ICountryService _countryService;
        private readonly ICityService _cityService;




        public AccountController(IUserService userService, IUnitOfWork unitOfWork, ICountryService countryService, ICityService cityService)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
            _countryService = countryService;
            _cityService = cityService;
        }


        [HttpGet]
        public async Task<IActionResult> Register()
        {
            var countries = await _countryService.FindAllCountries();
            var cities = await _cityService.FindAllCity();

            var model = new RegisterDto()
            {
                SelectListSourseCountry = new SelectList(countries, "Id", "Name"),
                SelectListSourseCity = new SelectList(cities, "Id", "Name")

            };




            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]// страница неявно внутри себя сгенерирует разметку, эта разметак будет передавать с собой соответсвующий токен где на BE он будет проверятся и если токен не сошелся, значет он пришел не со страницы, а откуда-то еще. т.е. если запрос пришел не сайта, то его обробатывать не нужно
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var country = await _countryService.FindCountryById(registerDto.CountrySourseId.Value);
            var city = await _cityService.FindCityById(registerDto.CitySourseId.Value);
            registerDto.Country = country.Name;
            registerDto.City = city.Name;

            if (country is null)
            {
                throw new NullReferenceException($"Country can't exist Null value");
            }

            if (city is null)
            {
                throw new NullReferenceException($"City can't exist Null value");
            }

            var hashSoult = _userService.GetPasswordHashSoult(registerDto.Password);

                if (await _userService.UserExist(registerDto.Login))
                {
                    return BadRequest("User allredy exist");
                }

                var newUser = await _userService.ArrangeNewUser(registerDto, hashSoult);

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
            return View(registerDto);
        }

    }
}
