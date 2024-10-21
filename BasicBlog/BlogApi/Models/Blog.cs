using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApi.Models
{
    [Table("Blogs")]
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string XLThumbNail { get; set; } = string.Empty;
        public string LThumbNail { get; set; } = string.Empty;
        public string MThumbNail { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public DateTime? PublishedAt { get; set; }
        public DateTime? ArchivedAt { get; set; }

        public BlogEnum Status { get; set; } = BlogEnum.Draft;

        public List<BlogCategory> BlogCategories { get; set;  } = new List<BlogCategory>();

    }

    public enum BlogEnum {
        Draft,
        Review,
        Publish,
        Archived
    }
}