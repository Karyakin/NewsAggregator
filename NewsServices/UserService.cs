﻿using Contracts.ServicesInterfacaces;
using Contracts.UnitOfWorkInterface;
using Entities.DataTransferObject;
using Entities.Entity.Users;
using Entities.Models;
using Entity.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
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
            var cityId = (await _cityService.FindCityByName(registerDto.City)).Id;
            var countryId = (await _countryService.FindCountryByName(registerDto.Country)).Id;

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

            try
            {
                var newUser = new User()
                {
                    Id = Guid.NewGuid(),
                    Login = registerDto.Login,
                    LastName = registerDto.LastName,
                    PasswordHash = passwordSoultModel.PasswordHash,
                    PasswordSalt = passwordSoultModel.PasswordSalt,
                    ContactDetailsId = contactDetailsId,

                    ContactDetails = new ContactDetails()
                    {
                        Id = contactDetailsId,
                        CountryId = countryId,
                        CityId = cityId,
                        EMails = emList,
                        Phones = phoneList

                    },
                    RoleId = (await _roleService.GetRoleIdyByName("User")).Id,
                    DayOfBirth = registerDto.DayOfBirth
                };
                return newUser;
            }
            catch (Exception ex)
            {
                Log.Error($"Can't create user. Details: {ex.Message}");
            }
            return new User();
        }

        public async Task<User> GetUserByLogin(string login) =>
            await _unitOfWork.User.GetByCondition(x => x.Login.Equals(login), false).FirstOrDefaultAsync();

        public async Task<User> GetUserWithDetails(string login) =>
                 await _unitOfWork.User.GetByCondition(x => x.Login.Equals(login), false)
                .Include(x => x.ContactDetails).ThenInclude(x => x.City)
                .Include(x => x.ContactDetails).ThenInclude(x => x.Country)
                .Include(x => x.ContactDetails).ThenInclude(x => x.EMails)
                .Include(x => x.ContactDetails).ThenInclude(x => x.Phones)
                .Include(x => x.Role)
                .FirstOrDefaultAsync();


        public async Task<IEnumerable<User>> GetAllUsersWithPhoneROleMail() =>
                 await _unitOfWork.User.GetAll(false)
                .Include(x => x.ContactDetails).ThenInclude(x => x.EMails)
                .Include(x => x.ContactDetails).ThenInclude(x => x.Phones)
                .Include(x => x.Role).ToListAsync();



        public void DeleteUser(User user)=> _unitOfWork.User.Remove(user);

        public async Task<User> GetUserById(Guid id)=>  await _unitOfWork.User.GetById(id, false);
        public async Task<IEnumerable<User>> GetAllUsers() => await _unitOfWork.User.GetAll(false).ToListAsync();

    }
}
