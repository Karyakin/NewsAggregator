using AutoMapper;
using Contracts.ServicesInterfacaces;
using Contracts.UnitOfWorkInterface;
using Entities.DataTransferObject;
using Entities.Entity.NewsEnt;
using Entities.Entity.Users;
using Entities.Models;
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
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

        public Task<User> ArrangeNewUser(PasswordSoultModel model, PasswordSoultModel passwordSoultModel)
        {
            throw new NotImplementedException();
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
