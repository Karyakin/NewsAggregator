using AutoMapper;
using Contracts.ServicesInterfacaces;
using Contracts.WrapperInterface;
using Entities.Entity.NewsEnt;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepositoryWrapper _wrapper;
       // private readonly IMapper _mapper;

        public CategoryService(IRepositoryWrapper wrapper, IMapper mapper)
        {
            _wrapper = wrapper;
         //   _mapper = mapper;
        }

        public async Task CreateManyCategories(IEnumerable<Category> categories)
        {
           _wrapper.Category.CreateMany(categories);
            await _wrapper.SaveAsync();

        }
        public async Task CreateOneCategory(Category category)
        {
           _wrapper.Category.Create(category);
            await _wrapper.SaveAsync();
        }
        public async Task<IEnumerable<Category>> GetAllCategoryAsync(bool trackChanges)
        {
            var categories = await _wrapper.Category.FindAll(trackChanges).ToListAsync();
            return categories;
        }

        public async Task<Category> FindCategoryByName(string categoryName) =>
            await _wrapper.Category.FindByCondition(x => x.Name.Equals(categoryName), true).FirstOrDefaultAsync();
    }
}
