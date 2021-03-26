using Contracts.RepositoryInterfaces;
using Entities.Entity.NewsEnt;
using Microsoft.EntityFrameworkCore;
using Repositories.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.RssSources
{
    public class RssSourceRepository : RepositoryBase<RssSource>, IRssSourceRepository
    {

        public RssSourceRepository(NewsDataContext newsDataContext) : base(newsDataContext)
        {
        }

        public void CreateManyRssSource(IEnumerable<RssSource> rssSource) => CreateMany(rssSource);

        public void CreateOneRssSource(RssSource rssSource) => Create(rssSource);

        public async Task<IEnumerable<RssSource>> GetAllRssSourceAsync(bool trackChanges) =>
           await FindAll(false).ToListAsync();

        public async Task<RssSource> FindRssSourceById(Guid rssSourceId) =>
            await FindByCondition(x => x.Id.Equals(rssSourceId), true).FirstOrDefaultAsync();

        public async Task<RssSource> FindRssSourceByName(string rssSourceName)=>
            await FindByCondition(x => x.Name.Equals(rssSourceName), true).FirstOrDefaultAsync();
    }
}
