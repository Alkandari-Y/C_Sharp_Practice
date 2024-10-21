using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApi.Dtos.Blog;
using BlogApi.Helpers;
using BlogApi.Interfaces;
using BlogApi.Mappers;
using BlogApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [Route("/api/blogs")]
    [ApiController]
    public class BlogControllers : ControllerBase
    {
        private readonly IBlogRepository _blogRepo;
        private readonly IBlogCategoryRepository _blogCategoryRepo;
        public BlogControllers(
            IBlogRepository blogRepo,
            IBlogCategoryRepository blogCategoryRepo
        )
        {
            _blogRepo = blogRepo;
            _blogCategoryRepo = blogCategoryRepo;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllBlogs()
        {
            var blogs = await _blogRepo.GetAllBlogsAsync();
            return Ok(
                blogs.Select(
                    blog => blog.ModelToSummaryDto()
                )
            );
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlog([FromBody] CreateBlogDto blogDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Blog blog = blogDto.CreateBlogDtoToModel();
            blog.Slug = SlugGenerator.GenerateSlug(blog.Title);

            await _blogRepo.CreateBlogAsync(blog);
            
            if (blogDto.CategoryIds.Count() >= 1) {
                var blogCategories = await _blogCategoryRepo.CreateBlogCategoriesASync(blog.Id, blogDto.CategoryIds);

                blog.BlogCategories = blogCategories;
            }

            return Ok(blog.ModelToSummaryDto());
        }

        [HttpGet]
        [Route("{blogSlug}")]
        public async Task<IActionResult> GetBlogDetailsBySlug([FromRoute] string blogSlug)
        {
            Blog? blog = await _blogRepo.GetBlogBySlug(blogSlug);

            if (blog is null) return NotFound();


            return Ok();
        }
    }
}