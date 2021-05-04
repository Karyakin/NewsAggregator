using Contracts.RepositoryInterfaces;
using Entity.Users;
using Repositories.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.CountryRepo
{
   public class CountryRepository : RepositoryBases<Country>, ICountryRepository
    {
        public CountryRepository(NewsDataContext newsDataContext)
           : base(newsDataContext)
        {
        }

       
    }
}
