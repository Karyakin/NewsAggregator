using Contracts.Interfaces;
using Entities.DataTransferObject;
using Entities.Entity.NewsEnt;
using Entities.Entity.Users;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ServicesInterfacaces
{
    public interface IUserService 
    {
        PasswordSoultModel GetPasswordHashSoult(string modelPassword);
        Task<bool> UserExist(string login);
        public Task<User> ArrangeNewUser(RegisterDto registerDto, PasswordSoultModel passwordSoultModel);
        Task<User> GetUserByLogin(string login);
        Task<User> GetUserWithDetails(string login);

        /*Task<bool> RegisterUser(UserDto model);
        */
    }
}
