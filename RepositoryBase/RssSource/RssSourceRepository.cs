using Contracts.RepositoryInterfaces;
using Entities.Entity.News;
using Microsoft.EntityFrameworkCore;
using Repositories.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.RssSource
{
   public class RssSourceRepository : RepositoryBase<RssSource>, IRssSourceRepository
    {

        public RssSourceRepository(NewsDataContext newsDataContext) : base(newsDataContext)
        {
        }

        public void CreateOneCategory(Entities.Entity.News.RssSource rssSource)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Entities.Entity.News.RssSource>> GetAllCategoryAsync(bool trackChanges)
        {
            throw new NotImplementedException();
        }
    }
}
