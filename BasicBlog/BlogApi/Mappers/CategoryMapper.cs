using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApi.Dtos.Category;
using BlogApi.Models;

namespace BlogApi.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryDto ModelToDto(this Category category)
        {
            return new()
            {
                Id = category.Id,
                Name = category.Name
            };
        }
    }
}