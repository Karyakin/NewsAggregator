using AutoMapper;
using Contracts.ServicesInterfacaces;
using Contracts.UnitOfWorkInterface;
using Entities.DataTransferObject;
using Entities.Entity.NewsEnt;
using Entities.Entity.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewsAggregatorMain.Helper;
using NewsAggregatorMain.Models.Account;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NewsAggregatorMain.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICountryService _countryService;
        private readonly ICityService _cityService;
        //private readonly IMapper _mapper;

        public AccountController(IUserService userService, IUnitOfWork unitOfWork, ICountryService countryService, ICityService cityService/*, IMapper mapper*/)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
            _countryService = countryService;
            _cityService = cityService;
           // _mapper = mapper;
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
            if (newUser is null)
            {
                return BadRequest(newUser);
            }
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

            return Ok($"User {registerDto.Login} was successfully");
        }


        [HttpGet]
        public IActionResult Login(string returnUrl)// returnUrl нужна для того, чтобы получать тот Url по которому клацнули изначально. Если пользователь не авторизован и нажал на источники, то после авторизации его перенаправит в источники, если нажал на новости, то после авторизации будет перенаправлен на новости.
        {
            var model = new LoginDto() { ReturnUrl = returnUrl };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Login(RegisterDto loginDto)
        {
            var user = await _unitOfWork.User.GetByCondition(x => x.Login == loginDto.Login, false).FirstOrDefaultAsync();
            if (user == null)
                return BadRequest();
            using (var hmac = new HMACSHA512(user.PasswordSalt))
            {
                var computesHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
                for (int i = 0; i < computesHash.Length; i++)
                {
                    if (computesHash[i] != user.PasswordHash[i])
                        return Unauthorized("Пользователь с таким именем не зарегистрирован!");//ошибка авторизации. не смог авторизоваться
                }
            }
            await Autointification(user);
            //  return RedirectToAction(nameof(Index), nameof(News));

            return string.IsNullOrEmpty(loginDto.ReturnUrl)
                           ? (IActionResult)RedirectToAction("Index", "Home")
                           : Redirect(loginDto.ReturnUrl);
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        /// <summary>
        /// Claim - базовый класс для работы с пользователями, содержит информацию для авторизации пользователя
        /// issuer - некий издатель, система, которая выдала нашему пользователю Claim. Доверянная система
        /// type - тип объекта клэйм
        /// subject - информация про то на основе чего мы будем авторизировать нашего пользователя
        /// value - Role.Value
        /// </summary>
        public async Task  Autointification(User user)
        {
            var age = AgeCount.GetAge(user);

            const string authType = "ApplicationCokie";
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim("age", age)
            };
            var identity = new ClaimsIdentity(
                claims,
                authType,
                ClaimsIdentity.DefaultRoleClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

           // return RedirectToAction(nameof(Index), nameof(News));
        }

        [HttpGet]
        public  IActionResult GetUserInfo()
        {
           /* if (string.IsNullOrEmpty(Login))
            {
                return BadRequest("Enret user login, please");
            }
            var user = await _userService.GetUserByLogin(Login);


            var countries = await _countryService.FindAllCountries();
            var cities = await _cityService.FindAllCity();
            var model = new UserDto()
            {
                SelectListSourseCountry = new SelectList(countries, "Id", "Name"),
                SelectListSourseCity = new SelectList(cities, "Id", "Name")

            };
            return View(model);*/
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetUserInfo(UserDto userDto)
        {
            /*  var country = await _countryService.FindCountryById(userDto.CountrySourseId.Value);
              var city = await _cityService.FindCityById(userDto.CitySourseId.Value);
              userDto.Country = country.Name;
              userDto.City = city.Name;*/
            if (string.IsNullOrEmpty(userDto.Login) || string.IsNullOrWhiteSpace(userDto.Login))
                return BadRequest("Enter valid user login");

            //var user = await _userService.GetUserByLogin(userDto.Login);
            var userWithDetails = await _userService.GetUserWithDetails(userDto.Login);
            if (userWithDetails is null)
                return BadRequest("User with this login does not exist");



            var filledUserRto = new UserDto()
            {
                Login = userWithDetails.Login,
                Role = userWithDetails.Role,
                Country = userWithDetails.ContactDetails.Country.Name,
                City = userWithDetails.ContactDetails.City.Name,
                
            };


            return View(filledUserRto);
        }
    }
}

