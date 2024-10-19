using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GameStore.Data;
using GameStore.Dtos.Game;
using GameStore.Mappers;
using GameStore.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Controllers;

public static class GameControllers
{

    public static RouteGroupBuilder MapGameControllers(this WebApplication app)
    {
        var group = app.MapGroup("games");

        group.MapGet("/", async (ApplicationDBContext dbContext) =>
            await dbContext.Games
                .Include(game => game.Genre)
                .Select((game) => game.ToGameSummaryDto())
                .AsNoTracking()
                .ToListAsync()
        );

        group.MapPost("/", async (CreateGameDto newGame, ApplicationDBContext dbContext) =>
        {
            Game game = newGame.ToEntity();

            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute(
                "GetGame",
                new { id = game.Id },
                game.ToGameDetailsDto()
            );
        }).WithParameterValidation();

        group.MapGet("/{id}", async (int id, ApplicationDBContext dbContext) =>
        {
            Game? game = await dbContext.Games.FindAsync(id);

            return game is null ?
                Results.NotFound()
                : Results.Ok(game.ToGameDetailsDto());
        }).WithName("GetGame");

        group.MapPut("/{id}", async (int id, UpdateGameDto updatedGameDTO, ApplicationDBContext dbContext) =>
        {
            Game? game = await dbContext.Games.FindAsync(id);

            if (game == null) return Results.NotFound();

            dbContext.Entry(game)
                .CurrentValues
                .SetValues(updatedGameDTO.ToEntity(id));
            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        }).WithParameterValidation();

        group.MapDelete("/{id}", async (int id, ApplicationDBContext dbContext) =>
        {
            await dbContext.Games
                .Where((game) => game.Id == id)
                .ExecuteDeleteAsync();

            return Results.NoContent();
        });

        return group;
    }

}

