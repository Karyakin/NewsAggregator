using Contracts.ServicesInterfacaces;
using Entities.Entity.Users;
using MediatR;
using NewsAgregato.DAL.CQRS.Queries;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.SQRS
{
    public class CQRSRoleService : IRoleService
    {

        private readonly IMediator _mediator;
        public CQRSRoleService(IMediator mediator)
        {
            _mediator = mediator;
        }


        public Task AddRoleToUser(string roleName, User user)
        {
            throw new NotImplementedException();
        }

        public async Task<Role> GetRoleIdyById(Guid idRole)
        {
            var roleByIdQuery = new GetRoleByIdQuery(idRole);
            if (roleByIdQuery is null)
            {
                Log.Error("failed to get the role");
                throw new ArgumentNullException();
            }
            var role = await _mediator.Send(roleByIdQuery);
            return role;
        }

        public Task<Role> GetRoleIdyByName(string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Role>> GetRoles()
        {
            throw new NotImplementedException();
        }
    }
}
