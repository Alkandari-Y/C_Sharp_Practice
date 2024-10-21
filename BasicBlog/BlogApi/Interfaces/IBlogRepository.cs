using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApi.Models;

namespace BlogApi.Interfaces
{
    public interface IBlogRepository
    {
        Task<List<Blog>> GetAllBlogsAsync();
        Task<Blog> CreateBlogAsync(Blog blog);
        Task<Blog?> GetBlogById(int Id);
    }
}