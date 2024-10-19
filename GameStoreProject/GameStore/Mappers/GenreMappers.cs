using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Models;
using GameStore.Dtos.Genre;

namespace GameStore.Mappers
{
    public static class GenreMappers
    {
        public static GenreDto ToDto(this Genre genre)
        {
            return new GenreDto(
                genre.Id,
                genre.Name
            );
        }

        public static Genre ToEntity(this CreateGenreDto genre)
        {
            return new Genre()
            {
                Name = genre.Name
            };
        }
    }
}