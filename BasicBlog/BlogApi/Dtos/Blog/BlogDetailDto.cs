using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApi.Models;

namespace BlogApi.Dtos.Blog
{
    public class BlogDetailDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required string Description { get; set; }
        public required string XLThumbNail { get; set; }
        public required string LThumbNail { get; set; }
        public required string MThumbNail { get; set; }
        public required string Slug { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? PublishedAt { get; set; }
        public DateTime? ArchivedAt { get; set; }

        public BlogEnum Status { get; set; }

        public List<BlogCategory> BlogCategories { get; set; } = new List<BlogCategory>();
    }
}