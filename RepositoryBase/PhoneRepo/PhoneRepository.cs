using Contracts.RepositoryInterfaces;
using Entities.Entity.Users;
using Repositories.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.PhoneRepo
{
    public class PhoneRepository : RepositoryBases<Phone>, IPhoneRepository
    {
        public PhoneRepository(NewsDataContext newsDataContext)
          : base(newsDataContext)
        {
        }
    }
}
