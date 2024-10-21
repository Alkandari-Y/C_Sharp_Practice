using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BlogApi.Models;

namespace BlogApi.Dtos.Blog
{
    public class CreateBlogDto
    {
        
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        [MaxLength(250)]
        public string Description { get; set; }
        [Required]
        [MinLength(250)]
        public string Content { get; set; }
        public string XLThumbNail { get; set; }
        [Required]
        public string LThumbNail { get; set; }
        [Required]
        public string MThumbNail { get; set; }
        [Required]
        public string Slug { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public DateTime? PublishedAt { get; set; }
        public DateTime? ArchivedAt { get; set; }

        [Required]
        public BlogEnum Status { get; set; }

        [MaxLength(1)]
        public List<int> CategoryIds { get; set;  } = new List<int>();
    }
}