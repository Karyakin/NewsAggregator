using Contracts.RepositoryInterfaces;
using Entities.Entity.NewsEnt;
using Microsoft.EntityFrameworkCore;
using Repositories.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Categories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(NewsDataContext newsDataContext) : base(newsDataContext)
        {
        }

        public void CreateManyCategories(IEnumerable<Category> categories)
        {
            CreateMany(categories);
        }

        public void CreateOneCategory(Category category)
        {
            Create(category);
        }

        public async Task<IEnumerable<Category>> GetAllCategoryAsync(bool trackChanges)
        {
            var res = await FindAll(trackChanges).ToListAsync();
            var res1 = res;
            return res1;
        }

      

        /*  public async Task<IEnumerable<Category>> GetAllCategoryAsync(bool trackChanges)
          {
              var res = await FindAll(trackChanges).ToListAsync();
              var res1 = res;
              return res1;
          }
          public async Task<IEnumerable<Category>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
          {
              var res = await FindByCondition(news => news.Id.Equals(ids), trackChanges).ToListAsync();
              return res;
          }
          public async Task<Category> GetCategoryAsync(Guid newsId, bool trackChanges)
          {
              var res = await FindByCondition(x => x.Id.Equals(newsId), trackChanges).SingleOrDefaultAsync();
              return res;
          }*/
    }
}
