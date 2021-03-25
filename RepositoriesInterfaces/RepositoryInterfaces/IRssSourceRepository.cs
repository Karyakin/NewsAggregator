using Entities.Entity.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.RepositoryInterfaces
{
   public interface IRssSourceRepository
    {
        Task<IEnumerable<RssSource>> GetAllCategoryAsync(bool trackChanges);
        void CreateOneCategory(RssSource rssSource);
    }
}
