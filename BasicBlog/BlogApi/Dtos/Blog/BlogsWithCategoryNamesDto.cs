using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApi.Models;

namespace BlogApi.Dtos.Blog
{
    public class BlogsWithCategoryNamesDto
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

        public string Status { get; set; } = string.Empty;

        public List<Category> Category  { get; set; } = new List<Category>();
    }
}