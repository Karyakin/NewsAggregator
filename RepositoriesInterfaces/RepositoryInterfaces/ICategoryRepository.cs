using Contracts.Interfaces;
using Entities.Entity.NewsEnt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.RepositoryInterfaces
{
    /// <summary>
    /// Сой вариант зависимостей и наследований
    /// </summary>
    public interface ICategoryRepository : IRepositoryBases<Category>
    {
      /*  Task<IEnumerable<Category>> GetAllCategoryAsync(bool trackChanges);
        void CreateOneCategory(Category category);
        void CreateManyCategories(IEnumerable<Category> categories);
        Task<Category> FindCategoryByName(string categoryName);*/
    }
}
