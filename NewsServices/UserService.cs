using AutoMapper;
using Contracts.Interfaces;
using Contracts.ServicesInterfacaces;
using Contracts.UnitOfWorkInterface;
using Entities.DataTransferObject;
using Entities.Entity.NewsEnt;
using Entities.Entity.Users;
using Entities.Models;
using Entity.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICountryService _countryService;
        private readonly ICityService _cityService;

        public UserService(IUnitOfWork unitOfWork, ICountryService countryService, ICityService cityService)
        {
            _unitOfWork = unitOfWork;
            _countryService = countryService;
            _cityService = cityService;
        }

        public PasswordSoultModel GetPasswordHashSoult(string modelPassword)
        {
            using (var hmac = new HMACSHA512())
            {
                PasswordSoultModel passeordSoult = new PasswordSoultModel()
                {
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(modelPassword)),
                    PasswordSalt = hmac.Key
                };
                return passeordSoult;
            }
        }
        public async Task<bool> UserExist(string login)
        {
            var exist = _unitOfWork.User.GetAll(false);
            var rez = await exist.AnyAsync(x => x.Login.Contains(login));
            return rez;
        }

        [ValidateAntiForgeryToken]
        public async Task<User> ArrangeNewUser(RegisterDto registerDto, PasswordSoultModel passwordSoultModel)
        {
/*
                var cityId = Guid.NewGuid();
            var countryId = Guid.NewGuid();*/
            var contactDetailsId = Guid.NewGuid();
            var eMailId = Guid.NewGuid();
            var phoneId = Guid.NewGuid();

            EMail mail = new EMail()
            {
                Id = eMailId,
                UserEMail = registerDto.Email,
                CreateDate = DateTime.Now,
                ContactDetailsId = contactDetailsId
            };

            Phone phone = new Phone()
            {
                Id = phoneId,
                PhoneNumber = registerDto.Phones,
                CreateDate = DateTime.Now,
                ContactDetailsId = contactDetailsId
            };

            List<EMail> emList = new List<EMail>() { mail };
            List<Phone> phoneList = new List<Phone>() { phone };

            var newUser = new User()
            {
                Id = Guid.NewGuid(),
                Login = registerDto.Login,
                PasswordHash = passwordSoultModel.PasswordHash,
                PasswordSalt = passwordSoultModel.PasswordSalt,
                ContactDetailsId = contactDetailsId,
                ContactDetails = new ContactDetails()
                {
                    Id = contactDetailsId,
                    City = new City()
                    {
                        Id = registerDto.CitySourseId.Value,
                        Name = registerDto.City
                    },

                    Country = new Country()
                    {
                        Id = registerDto.CountrySourseId.Value,
                        Name = registerDto.Country,
                       // CountryCod = registerDto.Country.
                        
                    },

                    EMails = emList,
                    Phones = phoneList
                },
            };
            return newUser;
        }










        public Task<RegisterDto> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RegisterUser(RegisterDto model)
        {
            throw new NotImplementedException();
        }

    }
}
