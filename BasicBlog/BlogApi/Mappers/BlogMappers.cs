using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApi.Dtos.Blog;
using BlogApi.Models;

namespace BlogApi.Mappers
{
    public static class BlogMappers
    {
        public static BlogSummaryDto ModelToSummaryDto(this Blog blog)
        {
            return new()
            {
                Id = blog.Id,
                Title = blog.Title,
                Description = blog.Description,
                XLThumbNail = blog.XLThumbNail,
                LThumbNail = blog.LThumbNail,
                MThumbNail = blog.MThumbNail,
                Slug = blog.Slug,
                CreatedAt = blog.CreatedAt,
            };
        }

        public static BlogDetailDto ModelToDetailDto(this Blog blog)
        {
            return new()
            {
                Id = blog.Id,
                Title = blog.Title,
                Content = blog.Content,
                Description = blog.Description,
                XLThumbNail = blog.XLThumbNail,
                LThumbNail = blog.LThumbNail,
                MThumbNail = blog.MThumbNail,
                Slug = blog.Slug,
                CreatedAt = blog.CreatedAt,
                UpdatedAt = blog.UpdatedAt,
                PublishedAt = blog.PublishedAt,
                ArchivedAt = blog.ArchivedAt,
                Status = blog.Status,
                BlogCategories = blog.BlogCategories
            };
        }

        public static Blog CreateBlogDtoToModel(this CreateBlogDto blog)
        {
            return new()
            {
                Title = blog.Title,
                Content = blog.Content,
                Description = blog.Description,
                XLThumbNail = blog.XLThumbNail,
                LThumbNail = blog.LThumbNail,
                MThumbNail = blog.MThumbNail,
                CreatedAt = blog.CreatedAt,
                UpdatedAt = blog.UpdatedAt,
                PublishedAt = blog.PublishedAt,
                ArchivedAt = blog.ArchivedAt,
                Status = blog.Status
            };
        }
    }
}