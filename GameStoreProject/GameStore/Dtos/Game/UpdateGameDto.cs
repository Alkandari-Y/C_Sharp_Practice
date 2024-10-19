using System.ComponentModel.DataAnnotations;

namespace GameStore.Dtos.Game;

public record class UpdateGameDto(
    [Required][StringLength(50)] string Name,
    [Required] int GenreId,
    [Range(1, 100)] decimal Price,
    DateOnly ReleaseDate
);