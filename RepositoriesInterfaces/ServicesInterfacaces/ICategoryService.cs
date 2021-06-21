using Entities.Entity.NewsEnt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ServicesInterfacaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoryAsync(bool trackChanges);
        Task<Category> FindCategoryByName(string categoryName);
        Task<List<Category>> CheckCategoriesForDublication(SyndicationFeed syndicationFeed);
        Task CreateOneCategory(Category category);
        Task CreateManyCategories(List<Category> categories);
    }
}
