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
        private readonly IRoleService _roleService;

        public UserService(IUnitOfWork unitOfWork, ICountryService countryService, ICityService cityService, IRoleService roleService)
        {
            _unitOfWork = unitOfWork;
            _countryService = countryService;
            _cityService = cityService;
            _roleService = roleService;
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
            var contactDetailsId = Guid.NewGuid();
            var eMailId = Guid.NewGuid();
            var phoneId = Guid.NewGuid();
           // var roleId = (_unitOfWork.Role.GetByCondition(x => x.Name.Equals("User"), false).FirstOrDefault()).Id;

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
                    CountryId = registerDto.CountrySourseId.Value,
                    CityId = registerDto.CitySourseId.Value,
                    EMails = emList,
                    Phones = phoneList
                    
                },
                RoleId = (await _roleService.GetRoleIdyByName("User")).Id,
              //DayOfBirth = registerDto.DayOfBirth
            };
            return newUser;
        }

        public async Task<User> GetUserByLogin(string login)=>
            await _unitOfWork.User.GetByCondition(x => x.Login.Equals(login), false).FirstOrDefaultAsync();

        public async Task<User> GetUserWithDetails(string login)
        {
            var user = await _unitOfWork.User.GetByCondition(x => x.Login.Equals(login), false)
                .Include(x => x.ContactDetails).ThenInclude(x=>x.City)
                .Include(x=>x.ContactDetails).ThenInclude(x=>x.Country)
                .Include(x=>x.ContactDetails).ThenInclude(x=>x.EMails)
                .Include(x=>x.ContactDetails).ThenInclude(x=>x.Phones)
                .Include(x=>x.Role)
                .FirstOrDefaultAsync();

            return user;
        }
    }
}
