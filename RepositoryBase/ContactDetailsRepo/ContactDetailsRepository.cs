using Contracts.ServicesInterfacaces;
using Entities.Entity.Users;
using Repositories.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.ContactDetailsRepo
{
   public class ContactDetailsRepository : RepositoryBases<ContactDetails>, IContactDetailsRepository
    {

        public ContactDetailsRepository(NewsDataContext newsDataContext)
           : base(newsDataContext)
        {
        }
        

    }
}
