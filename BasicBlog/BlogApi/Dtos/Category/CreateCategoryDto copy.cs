using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApi.Dtos.CategoryDtos
{
    public class UpdateCategoryDto
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; } = string.Empty;
    }
}