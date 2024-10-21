using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApi.Dtos.Category;
using BlogApi.Interfaces;
using BlogApi.Mappers;
using BlogApi.Models;
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

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto categoryDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var category = await _categoryRepo
                    .CreateCategoryAsync(
                        categoryDto.CreateCategoryDtoToModel()
            );

            return Ok(category.ModelToDto());
        }


        [HttpPut]
        [Route("{catId:int}")]
        public async Task<IActionResult> UpdateCategory(
            [FromRoute] int catId, 
            [FromBody] UpdateCategoryDto categoryDto
        )
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // var category = await _categoryRepo.GetCategoryById(catId);
            Category? category = await _categoryRepo.UpdateCategoryAsync(
                        catId, categoryDto);

            if (category == null) return NotFound("Category Not Found");

            return Ok(category.ModelToDto());
        }
    }
}