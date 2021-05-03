using AutoMapper;
using Contracts.ServicesInterfacaces;
using Contracts.UnitOfWorkInterface;
using Entities.Entity.NewsEnt;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
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

        /// <summary>
        /// Метода проводит проверку на наличие в базе названия категории и возвращает все категории без дубликатов
        /// </summary>
        /// <param name="syndicationFeed"></param>
        /// <returns></returns>
        public async Task<List<Category>> CheckCategoriesForDublication(SyndicationFeed syndicationFeed)
        {
            var categories = new List<Category>();

            if (syndicationFeed.Items.Any())
            {
                var category = await _wrapper.Category.GetAll(false).Select(z => z.Name).ToListAsync();

                foreach (var item in syndicationFeed.Items)
                {
                    foreach (var categoryName in item.Categories)
                    {
                        if (!category.Any(x => x.Equals(categoryName.Name)))
                        {
                            var catName = new Category()
                            {
                                Id = Guid.NewGuid(),
                                Name = categoryName.Name,
                            };
                            categories.Add(catName);
                        }
                    };
                }
                categories = categories.GroupBy(x => x.Name).Select(x => x.First()).ToList();
            }
            return categories;
        }


        public async Task CreateManyCategories(List<Category> categories)
        {
            _wrapper.Category.AddRange(categories);
            await _wrapper.SaveAsync();

        }
        public async Task CreateOneCategory(Category category)
        {
            _wrapper.Category.Add(category);
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
