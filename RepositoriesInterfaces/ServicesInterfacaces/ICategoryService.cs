using Entities.Entity.NewsEnt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ServicesInterfacaces
{

    /// <summary>
    /// Сой вариант зависимостей и наследований
    /// </summary>
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoryAsync(bool trackChanges);
        Task CreateOneCategory(Category category);
        Task CreateManyCategories(List<Category> categories);
        Task<Category> FindCategoryByName(string categoryName);


        Task<List<Category>> CheckCategoriesForDublication(SyndicationFeed syndicationFeed);
    }
}
