using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApi.Dtos.CategoryDtos;
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

        public static Category CreateCategoryDtoToModel(this CreateCategoryDto category)
        {
            return new()
            {
                Name = category.Name
            };
        }

        public static Category UpdateCategoryDtoToModel(this UpdateCategoryDto category)
        {
            return new()
            {
                Name = category.Name
            };
        }
    }
}