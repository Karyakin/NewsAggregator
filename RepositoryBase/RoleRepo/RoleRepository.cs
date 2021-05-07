using Contracts.ServicesInterfacaces;
using Contracts.UnitOfWorkInterface;
using Entities.Entity.Users;
using Repositories;
using Repositories.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RoleRepository : RepositoryBases<Role>, IRoleRepository
    {
        public RoleRepository(NewsDataContext newsDataContext)
           : base(newsDataContext)
        {
        }

     
    }
}
