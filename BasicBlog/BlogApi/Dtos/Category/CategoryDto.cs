using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApi.Dtos.Category
{
    public class CategoryDto
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
    }
}