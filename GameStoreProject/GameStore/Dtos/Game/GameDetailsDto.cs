using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Dtos.Game
{
    public record class GameDetailsDto(
        int Id,
        string Name,
        int GenreId,
        decimal Price,
        DateOnly ReleaseDate
    );
}