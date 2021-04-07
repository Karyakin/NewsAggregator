using Entities.Entity.NewsEnt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.RepositoryInterfaces
{
   public interface IRssSourceRepository 
    {
        Task<IEnumerable<RssSource>> GetAllRssSourceAsync(bool trackChanges);
        void CreateOneRssSource(RssSource rssSource);
        void CreateManyRssSource(IEnumerable<RssSource> rssSource);

        Task<RssSource> FindRssSourceById(Guid rssSourceId);
        Task<RssSource> FindNewsForSourse(Guid rssSourceId);
        Task<RssSource> FindRssSourceByName(string rssSourceName);


    }
}
