using Entities.Entity.NewsEnt;
using Entities.Models;
using Entities.Models.AssembledModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ServicesInterfacaces
{

    /// <summary>
    /// Сой вариант зависимостей и наследований
    /// </summary>
    public interface IRssSourceService// : IRssSourceService
    {
        Task<IEnumerable<RssSourceModel>> GetAllRssSourceAsync(bool trackChanges);
        Task CreateOneRssSource(RssSourceModel rssSourceModel);
        Task CreateManyRssSource(IEnumerable<RssSource> rssSource);

        Task<RssSourceModel> RssSourceById(Guid? rssSourceId);
        Task<SourseWithNewsCategory> RssSourceByIdWithNews(Guid? rssSourceId);
        Task<RssSource> RssSourceByName(string rssSourceName);
    }
}
