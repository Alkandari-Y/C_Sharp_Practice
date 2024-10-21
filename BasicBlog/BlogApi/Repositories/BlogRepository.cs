using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApi.Data;
using BlogApi.Interfaces;
using BlogApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Repositories
{
    public class BlogRepository : IBlogRepository
    {

        private readonly ApplicationDBContext _context;

        public BlogRepository(ApplicationDBContext context)
        {
            _context = context;            
        }

        public async Task<Blog> CreateBlogAsync(Blog blog)
        {
            await _context.Blogs.AddAsync(blog);
            await _context.SaveChangesAsync();
            
            return blog;
        }

        public async Task<List<Blog>> GetAllBlogsAsync()
        {
            return await _context.Blogs.ToListAsync();
        }

        public async Task<Blog?> GetBlogById(int Id)
        {
            Blog? blog = await _context.Blogs.FindAsync(Id);

            return blog ?? null;
        }
    }
}