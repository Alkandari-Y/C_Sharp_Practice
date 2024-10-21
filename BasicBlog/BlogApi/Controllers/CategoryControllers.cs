using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApi.Inferfaces;
using BlogApi.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [Route("/api/categories")]
    [ApiController]
    public class CategoryControllers : ControllerBase
    {

        private readonly ICategoryRepository _categoryRepo;
        public CategoryControllers(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryRepo.GetAllCategoriesAsync();
            return Ok(
                categories.Select(
                    category => category.ModelToDto()
                )
            );
        }
    }
}