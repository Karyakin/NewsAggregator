using Entities.Entity.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewsAgregato.DAL.CQRS.Queries;
using Repositories.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAgregato.DAL.CQRS.QueryHendlers
{
    public class GetRoleByUserQueryHendler : IRequestHandler<GetRoleByIdQuery, Role>
    {
        private readonly NewsDataContext _dbContext;

        public GetRoleByUserQueryHendler(NewsDataContext dbContext)
        {
            _dbContext = dbContext;
        }

       

        public async Task<Role> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var roleForUser = await _dbContext.Roles.FirstOrDefaultAsync(sou => sou.Id.Equals(request.Id));
            return roleForUser;
        }
    }
}
