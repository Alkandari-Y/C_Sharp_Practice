using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Dtos.Genre;

public record CreateGenreDto(
    [Required] string Name
);