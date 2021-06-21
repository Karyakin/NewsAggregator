using Entities.Entity.NewsEnt;
using Entities.Models;
using Entities.Models.AssembledModel;
using Microsoft.AspNetCore.Mvc;
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
        Task<RssSourceModel> GetRssSourceById(Guid? rssSourceId);
        Task<SourseWithNewsCategory> RssSourceByIdWithNews(Guid? rssSourceId);
        Task<IEnumerable<RssSourceModel>> RssSourceByNameAndUrl(string name, string url);
        Task<int> DeleteRssSourse(Guid id);
    }
}
