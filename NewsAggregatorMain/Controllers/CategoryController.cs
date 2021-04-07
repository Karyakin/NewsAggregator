using AutoMapper;
using Contracts.ServicesInterfacaces;
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
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var res = await _categoryService.GetAllCategoryAsync(false);
            return Ok(res);
        }

       /* [HttpGet]
        public async Task<IActionResult> GetOneNews()
        {
            return  await _
        }*/

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = await _categoryService.GetAllCategoryAsync(false);
            return Ok(res);
        }

        [HttpPut]
        public async Task<IActionResult> AddCategory()
        {
            Category category = new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Музыка",
                Description = "Пляски гулянки"
            };

           await _categoryService.CreateOneCategory(category);

            return Ok($"Новая категория {category.Name} была успешно дабавлена");
        }

        [HttpPut]
        public async Task<IActionResult> AddCategories()
        {

            Category medical = new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Медицина",
                Description = "Все о медицине"
            };
            Category worl = new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Мир",
                Description = "Че там в мире"
            };

            /* Category sport = new Category()
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
             };*/

            List<Category> categories = new List<Category>()
            {
              medical,
              worl,
            };

            await _categoryService.CreateManyCategories(categories);
            return Ok();

        }
    }
}
