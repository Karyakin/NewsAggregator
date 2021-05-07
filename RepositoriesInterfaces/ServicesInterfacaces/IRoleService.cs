using Entities.Entity.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ServicesInterfacaces
{
    public interface IRoleService
    {
        Task<Role> GetRoleIdyByName(string roleName);
    }
}
