using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApi.Models;

namespace BlogApi.Interfaces
{
    public interface IBlogCategoryRepository
    {
        Task<List<BlogCategory>> CreateBlogCategoriesASync(int blogId, List<int> categoryIds);
    }
}