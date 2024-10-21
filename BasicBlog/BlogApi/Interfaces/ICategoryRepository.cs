using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApi.Dtos.Category;
using BlogApi.Models;

namespace BlogApi.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategoriesAsync();
        Task<Category> CreateCategoryAsync(Category category);
        Task<Category?> GetCategoryById(int Id);
        Task<Category?> UpdateCategoryAsync(int categoryId, UpdateCategoryDto updatedCategory);
    }
}