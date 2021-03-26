using Contracts.WrapperInterface;
using Entities.Entity.NewsEnt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.NewsRep

{
    public class CreateNewsWhithCategorySource
    {
        private readonly IRepositoryWrapper _wrapper;
        public CreateNewsWhithCategorySource(IRepositoryWrapper wrapper)
        {
            _wrapper = wrapper;
        }
        public async Task CreateFullNews(string categoryName, string rssSourceName)
        {
            var sourse = await _wrapper.RssSource.FindRssSourceByName(categoryName);


        }
    
    }
}
