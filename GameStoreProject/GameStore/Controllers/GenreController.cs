using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GameStore.Data;
using GameStore.Dtos.Genre;
using GameStore.Mappers;
using GameStore.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Controllers
{
    public static class GenreController
    {
        public static RouteGroupBuilder MapGenreControllers(this WebApplication app)
        {
            var group = app.MapGroup("genres");

            group.MapGet("/", async (ApplicationDBContext dbContext) =>
                await dbContext.Genres
                    .Select((genre) => genre.ToDto())
                    .AsNoTracking()
                    .ToListAsync()
            );


            group.MapPost("/", async (CreateGenreDto newGenre, ApplicationDBContext dbContext) => {
                Genre genre = newGenre.ToEntity();
                await dbContext.AddAsync(genre);
                await dbContext.SaveChangesAsync();

                return Results.Ok(genre.ToDto());
            });
            return group;
        }
    }
}