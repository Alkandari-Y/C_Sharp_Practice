using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApi.Data;
using BlogApi.Dtos.CategoryDtos;
using BlogApi.Interfaces;
using BlogApi.Models;
using Microsoft.EntityFrameworkCore;


namespace BlogApi.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDBContext _context;
        public CategoryRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetCategoryById(int Id)
        {
            Category? category = await _context.Categories.FindAsync(Id);

            return category ?? null;
        }

        public async Task<Category?> UpdateCategoryAsync(
            int catId,
            UpdateCategoryDto updatedCategory
        )
        {
            Category? category = await _context.Categories.FindAsync(catId);
            if (category == null) return null;

            _context.Categories
                    .Entry(category)
                    .CurrentValues
                    .SetValues(updatedCategory);
            
            await _context.SaveChangesAsync();

            return category;
        }
    }
}