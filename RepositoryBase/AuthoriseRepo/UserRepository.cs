using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.RepositoryInterfaces;
using Repositories.Context;
using Entities.Entity.NewsEnt;
using Entities.Entity.Users;

namespace Repositories.NewsRep
{
    public class UserRepository : RepositoryBases<User>, IUserRepository
    {
        public UserRepository(NewsDataContext newsDataContext)
            : base(newsDataContext)
        {
        }





      //  https://www.youtube.com/watch?v=S4YDarQBkiM&ab_channel=CodeMaze

      
    }
}
