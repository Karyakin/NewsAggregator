using Entities.Entity.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.RepositoryInterfaces
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        Task<IEnumerable<Category>> GetAllCategoryAsync(bool trackChanges);
        void CreateOneCategory(Category category);
    }
}
