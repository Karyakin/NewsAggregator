using Contracts.RepositoryInterfaces;
using Entities.Entity.NewsEnt;
using Microsoft.EntityFrameworkCore;
using Repositories.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.RssSources
{
    public class RssSourceRepository : RepositoryBases<RssSource>, IRssSourceRepository
    {

        public RssSourceRepository(NewsDataContext newsDataContext) : base(newsDataContext)
        {
        }
     
    }
}
