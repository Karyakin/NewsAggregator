using Entities.Entity.NewsEnt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ServicesInterfacaces
{
   public interface IRssSourceService
    {
        Task<IEnumerable<RssSource>> GetAllRssSourceAsync(bool trackChanges);
        Task CreateOneRssSource(RssSource rssSource);
        Task CreateManyRssSource(IEnumerable<RssSource> rssSource);

        Task<RssSource> RssSourceById(Guid rssSourceId);
        Task<RssSource> RssSourceByName(string rssSourceName);
    }
}
