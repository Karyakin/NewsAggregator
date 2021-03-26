using AutoMapper;
using Contracts.WrapperInterface;
using Entities.Entity.NewsEnt;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAggregatorMain.Controllers
{

    public class CategoryController : Controller
    {
        private readonly IRepositoryWrapper _wrapper;
        private readonly IMapper _mapper;

        public CategoryController(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _wrapper = repositoryWrapper;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var res = await _wrapper.Category.GetAllCategoryAsync(false);


            return Ok(res);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var res = await _wrapper.Category.GetAllCategoryAsync(false);


            return Ok(res);
        }

        [HttpPut]
        public async Task<IActionResult> AddCategory()
        {
            Category category = new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Экономика",
                Description = "Все о больших и маленьких деньгах"
            };


            _wrapper.Category.CreateOneCategory(category);
            await _wrapper.SaveAsync();

            return Ok($"Новая категория {category.Name} была успешно дабавлена");
        }

        [HttpPut]
        public async Task<IActionResult> AddCategories()
        {

            Category political = new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Политика",
                Description = "Наиболее заметные новости в мире политики"
            };
            Category social = new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Общество",
                Description = "Наиболее заметные новости в обществе"
            };
            Category sport = new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Спорт",
                Description = "Все, что происходит в мире спорта"
            };
            Category art = new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Искуство",
                Description = "Наиболее актуальное из мира исскуства"
            };

            List<Category> categories = new List<Category>()
            {
              political,
              social,
              sport,
              art
            };

            _wrapper.Category.CreateManyCategories(categories);
            await _wrapper.SaveAsync();
            return Ok();

        }
    }
}
