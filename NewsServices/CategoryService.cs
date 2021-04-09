using AutoMapper;
using Contracts.ServicesInterfacaces;
using Contracts.UnitOfWorkInterface;
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
        private readonly IUnitOfWork _wrapper;
       // private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork wrapper, IMapper mapper)
        {
            _wrapper = wrapper;
         //   _mapper = mapper;
        }

        public async Task CreateManyCategories(IEnumerable<Category> categories)
        {
           await _wrapper.Category.AddRange(categories);
            await _wrapper.SaveAsync();

        }
        public async Task CreateOneCategory(Category category)
        {
           await _wrapper.Category.Add(category);
            await _wrapper.SaveAsync();
        }
        public async Task<IEnumerable<Category>> GetAllCategoryAsync(bool trackChanges)
        {
            var categories = await _wrapper.Category.GetAll(trackChanges).ToListAsync();
            return categories;
        }

        public async Task<Category> FindCategoryByName(string categoryName) =>
            await _wrapper.Category.GetByCondition(x => x.Name.Equals(categoryName), true).FirstOrDefaultAsync();
    }
}
