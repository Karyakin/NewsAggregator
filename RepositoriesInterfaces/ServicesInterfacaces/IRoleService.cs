using Entities.DataTransferObject;
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
        Task<Role> GetRoleIdyById(Guid idRole);
        Task AddRoleToUser(string roleName, User user);
     
        Task<IEnumerable<Role>> GetRoles();
    }
}
