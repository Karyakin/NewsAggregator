using Entities.Entity.NewsEnt;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ServicesInterfacaces
{
   public interface IRssSourceService
    {
        Task<IEnumerable<RssSourceModel>> GetAllRssSourceAsync(bool trackChanges);
        Task CreateOneRssSource(RssSourceModel rssSourceModel);
        Task CreateManyRssSource(IEnumerable<RssSource> rssSource);

        Task<RssSourceModel> RssSourceById(Guid? rssSourceId);
        Task<RssSource> RssSourceByName(string rssSourceName);
    }
}
