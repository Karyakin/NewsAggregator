using Contracts.Interfaces;
using Entities.DataTransferObject;
using Entities.Entity.NewsEnt;
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
        string GetPasswordHash(string modelPassword);
        /*Task<bool> RegisterUser(UserDto model);
        Task<UserDto> GetUserByEmail(string email);*/
    }
}
