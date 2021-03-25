using AutoMapper;
using Contracts.WrapperInterface;
using Entities.Entity.News;
using Microsoft.AspNetCore.Mvc;
using System;
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
                Name = "Политика",
                Description = "Наиболее заметные новости в мире политики"
            };

             _wrapper.Category.CreateOneCategory(category);
            await _wrapper.SaveAsync();

            return Ok($"Новая категория {category.Name} была успешно дабавлена");
        }
    }
}
