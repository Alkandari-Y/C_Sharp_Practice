using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApi.Data;
using BlogApi.Interfaces;
using BlogApi.Models;

namespace BlogApi.Repositories
{
    public class BlogCategoryRepository : IBlogCategoryRepository
    {

        private readonly ApplicationDBContext _context;
        public BlogCategoryRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<BlogCategory>> CreateBlogCategoriesASync(int blogId, List<int> categoryIds)
        {
            var blogCategories = categoryIds.Select(catId => new BlogCategory()
            {
                CategoryId = catId,
                BlogId = blogId
            }).ToList();


            await _context.BlogCategories.AddRangeAsync(blogCategories);
            await _context.SaveChangesAsync();

            return blogCategories;
        }
    }
}