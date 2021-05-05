using Contracts.Interfaces;
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
    public class CityRepository : RepositoryBases<City>, ICityRepository
    {
       
        public CityRepository(NewsDataContext newsDataContext)
            : base(newsDataContext)
        {
        }

       /* public Task<bool> CityExist(string login)
        {
            throw new NotImplementedException();
        }*/
    }
}
