using AutoMapper;
using Contracts.ServicesInterfacaces;
using Contracts.UnitOfWorkInterface;
using Entities.Entity.NewsEnt;
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

        public string GetPasswordHash(string modelPassword)
        {
            var sha256 = new SHA256CryptoServiceProvider();
            var sha256data = sha256.ComputeHash(Encoding.UTF8.GetBytes(modelPassword));
            var hashedPassword = Encoding.UTF8.GetString(sha256data);
            return hashedPassword;

        }

        public Task<UserDto> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RegisterUser(UserDto model)
        {
            throw new NotImplementedException();
        }
    }
}
