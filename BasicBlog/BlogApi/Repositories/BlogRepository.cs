using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApi.Data;
using BlogApi.Interfaces;
using BlogApi.Models;

namespace BlogApi.Repositories
{
    public class BlogRepository : IBlogRepository
    {

        private readonly ApplicationDBContext _context;

        public BlogRepository(ApplicationDBContext context)
        {
            _context = context;            
        }

        public Task<Blog> CreateBlogAsync(Blog blog)
        {
            throw new NotImplementedException();
        }

        public Task<List<Blog>> GetAllBlogsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Blog?> GetBlogById(int Id)
        {
            throw new NotImplementedException();
        }
    }
}