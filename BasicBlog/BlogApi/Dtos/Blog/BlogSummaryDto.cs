using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApi.Models;

namespace BlogApi.Dtos.Blog
{
    public class BlogSummaryDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string XLThumbNail { get; set; }
        public required string LThumbNail { get; set; }
        public required string MThumbNail { get; set; }
        public required string Slug { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}