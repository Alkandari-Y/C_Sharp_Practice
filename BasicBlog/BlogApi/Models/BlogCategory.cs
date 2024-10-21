using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApi.Models
{
    [Table("BlogCategories")]
    public class BlogCategory
    {
        public int Id { get; set; }
        [ForeignKey("Blog")]
        public int BlogId { get; set; }
         [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public Blog? Blog { get; set; }
        public Category? Category { get; set; }

    }
}