using Contracts.RepositoryInterfaces;
using Entity.Users;
using Repositories.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EmailRepo
{
   public class EmailRepository : RepositoryBases<EMail>, IEmailRepository
    {

        public EmailRepository(NewsDataContext newsDataContext)
           : base(newsDataContext)
        {
        }
    }
}
