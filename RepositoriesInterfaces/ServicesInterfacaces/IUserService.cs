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


        /*Task<bool> RegisterUser(UserDto model);
        Task<UserDto> GetUserByEmail(string email);*/
    }
}
