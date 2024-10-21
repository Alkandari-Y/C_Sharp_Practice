# README

## Setup

Install the following tools

- Install the [.NET runtime](https://learn.microsoft.com/en-us/dotnet/core/install/)
- Install [Microsoft SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
  - Choose Express (covers most cases) or Developer
- Install [Microsoft SMSS](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16)
- Install the `ef` cli tools

```bash
dotnet tool install --global dotnet-ef
```

### VS Code Extensions

Install the following extensions

- C# Dev Kit - By Microsoft, includes:
  - C# - Microsoft
  - C# Dev Kit - Microsoft
  - IntelliCode for C# Dev Kit - Microsoft
  - .NET Install Tool - Microsoft
  - .NET Extensions Pack - Microsoft
  - C# Extensions - JosKreativ

## Commands

### Dotnet CLI-Commands

Ensure the dotnet SDK is installed for the following commands

- Get version

```bash
dotnet --version
```

- Get verbose information

```bash
dotnet --info
```

- **Create a new project** from a template, provides list of available project templates

```bash
dotnet new list
```

- Create a project

```bash
dotnet new  <template_name>
```

- Create a project within a directory

```bash
dotnet new webapi -o <dir_name>
# or
dotnet new blazorwasm -o <dir_name>
```

- Build the project

```bash
dotnet build
```

- Add a package to the project

```bash
dotnet add package <package_name> --version <version_number>
```

- Run the project

```bash
dotnet run
# or
dotnet watch run
```

- Initialize Migrations

```bash
dotnet ef migrations add init
# or
dotnet ef migrations add InitialCreate --output-dir Data\Migrations
```

- Run database migrations

```bash
dotnet ef database update
```

- To undo migrations

```bash
dotnet ef migrations remove
```

### VS Code Commands

- To **create a new project**:

  - Press `Ctrl+Shift+P` to open the command palette
  - Type `.NET: New Project`
  - Select the desired project template
    - `ASP.NET Core Empty` for clean backend templates
    - `ASP.NET Core API` for basic backend template for API development with Swagger
  - Select a directory for the project
  - Assign a project name

- To **build the project**

  - Press `Ctrl+Shift+P` to open the command palette
  - .NET: build

- To run the application
  - Press `F5`
  - Select `C#` as the debugger
  - Select `Default Configuration`

## Adding Models & Connecting to a Database

### Creating Database Models

- Create a directory called models
- Create classes for each model and annotate accordingly using

```cs
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicStore.Models
{
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        [Column(TypeName="decimal(18,2)")]
        public decimal NetWorth { get; set; }

        public List<Song> Songs { get; set; } = new List<Song>();
    }
}
```

### Create the Database Context

- Create a directory called `Data`
- Create a file called `ApplicationDBContext`
- Create a model called `APplicationDBContext` which inherits from `DbContext`
- Create a constructor with `dbContextOptions` of type `DbContextOptions` and pass it to `base`
- Create `DbSets` for each model

```cs
using Microsoft.EntityFrameworkCore;
using MusicStore.Models;

namespace MusicStore.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
        {

        }

        public DbSet<Song> Songs { get; set; }
        public DbSet<Artist> Artists { get; set; }
    }
}
```

**Note:** Alternative Syntax

```cs
namespace MusicStore.Data;
public class ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
    : DbContext(options)
{
    public DbSet<Song> Songs => Set<Song>();
    public DbSet<Artist> Artists => Set<Artist>();
}

```

### SQL Server Setup

Main Dependencies for `Entity Framework` include:

```
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
Microsoft.EntityFrameworkCore.Design
```

**NOTE:** Ensure you match the versions with the dotnet version of dotnet mentioned in `csproj` file

- Installing Dependencies for entity framework
  - [Microsoft.EntityFrameworkCore.SqlServer](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/9.0.0-rc.2.24474.1)
  ```bash
  dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.8
  ```
  - [Microsoft.EntityFrameworkCore.Tools](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Tools/9.0.0-rc.2.24474.1)
  ```bash
  dotnet add package Microsoft.EntityFrameworkCore.Tools --version 8.0.8
  ```
  - [Microsoft.EntityFrameworkCore.Design](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Design/9.0.0-rc.2.24474.1)
  ```bash
  dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.8
  ```
- Install the following for Nested Responses and serialization

  - [Newtonsoft.json](https://www.nuget.org/packages/Newtonsoft.Json)

  ```bash
  dotnet add package Newtonsoft.Json --version 13.0.3
  ```

  - [Microsoft.AspNetCore.Mvc.NewtonsoftJson](https://www.nuget.org/packages/Microsoft.AspNetCore.Mvc.NewtonsoftJson/9.0.0-rc.2.24474.3)

  ```bash
  dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson --version 8.0.8
  ```

  - Add configuration to the `Project.cs` file

  ```cs
    builder.Services.AddControllers().AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });
  ```

- Add the configuration at the application level to inject the database context

```cs
// Project.cs

//...
// Inject the Database
builder.Services.AddDbContext<ApplicationDBContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
})

var app = builder.Build();
// ...
```

- Create the MSQL Database
  - Windows
    - Open the SSMS application, and right-click on the Databases tab to create a database
    - modify the `appsettings.json` by adding a new property of `ConnectionStrings`, and assign a new object with a property of `DefaultConnection`
    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Data Source=<REPLACE_WITH_OWNER>\\SQLEXPRESS;Initial Catalog=<REPLACE_WITH_DBNAME>;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
      }
    }
    ```

### SQLite Setup

Main Dependencies for `Entity Framework` include:

```
Microsoft.EntityFrameworkCore.Sqlite

```

- Installing Dependencies for entity framework
  - [Microsoft.EntityFrameworkCore.Sqlite](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Sqlite/9.0.0-rc.2.24474.1#readme-body-tab)
  ```bash
  dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 8.0.8
  ```
  - [Microsoft.EntityFrameworkCore.Design](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Design/9.0.0-rc.2.24474.1)
  ```bash
  dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.8
  ```
- Add sqlite to the application
  ```cs
  builder.Services.AddSqlite<ApplicationDBContext>
  ```
- Add the configuration at the application level to inject the database context

  ```cs
  builder.Services.AddSqlite<ApplicationDBContext>(
      builder.Configuration.GetConnectionString("DefaultConnection")
  );
  ```

- Modify the `appsettings.json` by adding a new property of `ConnectionStrings` and assign a new object with a property of `DefaultConnection`:
  ```json
  {
    "ConnectionStrings": {
      "DefaultConnection": "Data Source=sqlite.db"
    }
  }
  ```

### Extend for Auto-Migrations

- Create a file and a static class called `DataExtensions`
- Create a static void method called `MigrateDb` the extends the app object
- Create a `scoped service` object
- Get the database service
- Run migrations

  ```cs
  using Microsoft.EntityFrameworkCore;

  namespace MusicStore.Data;
  public static class DataExtensions
  {
      public static void MigrateDb(this WebApplication app)
      {
          var scope = app.Services.CreateScope();
          var dbContext = scope.ServiceProvider.GetService<ApplicationDBContext>();
          if (dbContext == null) {
              return;
          }
          dbContext.Database.Migrate();
      }
  }
  ```

- Invoke the `MigrateDb` extension method before the `Run` method in the `Program.cs` file

```cs
app.MigrateDb();
app.Run();
```

### MigrateAsync

- To Convert The `Migrate` extension method to an async function, we now need to modify the declaration to be `async` and return a `Task`
- Any database operations need to be awaited

```cs
namespace MusicStore.Data
{
    public static class DataExtensions
    {
        public static async Task MigrateDbAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<ApplicationDBContext>();
            if (dbContext == null) {
                return;
            }
            await dbContext.Database.MigrateAsync();
        }
    }
}
```

- Reflect the changes to the method name in the `Project.cs` file and await the function call

```cs
await app.MigrateDbAsync();

app.Run();
```

### Modify Logging for EntityFrameworkCore

- Include a key, value pair in the `appsettings.json` under `Logging` with the key of `Microsoft.EntityFrameworkCore.Database.Command` and assign the `Warning` level

```json
{
  "Logging": {
    "LogLevel": {
      "Microsoft.EntityFrameworkCore.Database.Command": "Warning"
    }
  }
}
```

### Seeding Data

- Create an initial migration
- Run your initial Migration
- Overwrite the `OnModelCreating` method within the `ApplicationDBContext class` to modify the behavior of `EntityFramework`
- Within the body, use `modelBuilder` object and get the `Entity` or model you want to populate and chain the `HasData` method
- Include all the objects you want as initial seeds to be separate arguments for the `HasData` method

```cs
using MusicStore.Models;
using Microsoft.EntityFrameworkCore;

namespace MusicStore.Data
{
    public class ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : DbContext(options)
    {
        public DbSet<Artist> Artists => Set<Artist>();
        public DbSet<Genre> Genres => Set<Genre>();
        public DbSet<Song> Songs => Set<Song>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>().HasData(
                new { Id = 1, Name = "Pop" },
                new { Id = 2, Name = "Rock" },
                new { Id = 3, Name = "Rap" },
                new { Id = 4, Name = "RnB" },
                new { Id = 5, Name = "Soul" },
                new { Id = 6, Name = "Metal" },
                new { Id = 7, Name = "Classical" }
            );
        }
    }
}
```

- Run a new migration

```bash
dotnet ef migrations add SeedGenres --output-dir Data\Migrations
```

## DTOs

### Create DTOs

1. Create a directory called `Dtos`
2. Create a file and a base `Dto` class per entity, e.g. `ArtistDto`, `GenreDto`, `SongDto`
3. Create a `Dto` per endpoint requirements, and optionally add annotations where needed to improve validation

```cs
using System.ComponentModel.DataAnnotations;

namespace MusicStore.Dtos;

public record class CreateSongDto(
    [Required][StringLength(50)]string Name,
    // Change this to type int if the database is connected
    [Required][StringLength(20)]string Genre,
    [Required][StringLength(20)]string Artist,
    [Range(1, 100)]decimal Duration,
    DateOnly ReleaseDate
);
```

4. To enforce annotations, we can add the [MinimalApis.Extensions](https://www.nuget.org/packages/MinimalApis.Extensions) package

```bash
dotnet add package MinimalApis.Extensions --version 0.11.0
```

5. Use the `WithParameterValidation` method at the end of each endpoint that requires validation

```cs
app.MapPost("/", (CreateSongDto newSong) =>
{
    // logic
}).WithParameterValidation()
```

## Creating Endpoints

### Starter Endpoints - Basic

You can create endpoints in with the `Map<HTTP_Verb>` method in the `Program.cs` file

```cs
// Argument in the post extracts data from the body
app.MapPost("songs", (CreateSongDto newSong) => {
    MusicDto Music = new(
        newSong.Count + 1,
        newSong.Name,
        newSong.Genre,
        newSong.Artist,
        newSong.Duration,
        newSong.ReleaseDate
        );
    songs.Add(newSong);
    // Return response object
    return Results.Created();
});
```

To redirect the user to another router following the create method above, we can use a different method from the `Response` class and also modify/create the detail route, to fetch the newly created object

```cs
app.MapPost("songs", (CreateSongDto newSong) => {
    SongDto song = new(
        songs.Count + 1,
        newSong.Name,
        newSong.Genre,
        newSong.Artist,
        newSong.Duration,
        newSong.ReleaseDate
        );
    newSong.Add(song);
    // Similar to Results.Created() where it adds a status code, it adds a location header of the detail page
    // requires routeName and an object for route params/arguments if needed
    // can include an optional argument of the payload of the newly created object
    return Results.CreatedAtRoute("GetSong", new { id = song.Id }, song);
});

// Param of id extracted from URL
app.MapGet("songs/{id}", (int id) => {
    SongDto? song = songs.Find((song) => song.Id == id);

    return song is null ? Results.NotFound() : Results.Ok(song);
}).WithName("GetSong");

// WithName method assigns a unique name to the route
```

#### Extending the App Object - Basic

- Create a folder to contain all endpoints, e.g. `Controllers`, `Endpoints`, `APIs`, etc.
- Create a class for the endpoints with appropriate naming related to the main entity
- Assign a namespace, e.g. `MusicStore.Controllers`
- To Extend the `app` object, create a `public static class`
- Define the extension method called `Map<Something>` that receives and returns a `WebApplication` instance, in the body, this will extend the app's functionality and allow it to call this method

```cs
// MusicStore/Song/Controllers
namespace MusicStore.Controllers;
public static class SongControllers {
    ...
    public static WebApplication MapSongControllers(this WebApplication app) {
        group.MapGet("songs", () => songs);
        // add all methods here onto the app, e.g. app.MapPost(...)
        return app;
    }
}
```

- In the `Program.cs` file, call the Map methods to map the endpoints to the app

```cs
using MusicStore.Controllers;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Called from the app
app.MapSongControllers();

app.Run();
```

- Alternatively, we can create a group for the endpoint such that we create a group of related routes using a `RouteGroupBuilder`:

```cs
    public static RouteGroupBuilder MapSongControllers(this WebApplication app)
    {
        var group = app.MapGroup("songs");

        group.MapGet("/", () => songs);
        group.MapPost("/", (CreateSongDto newSong) =>
        {
            // logic
        });

        group.MapGet("/{id}", (int id) =>
        {
            // logic

        }).WithName("GetSong");

        group.MapPut("/{id}", (int id, UpdateSongDto updatedSong) =>
        {
            // logic
        });

        group.MapDelete("/{id}", (int id) =>
        {
            // logic
        });

        return group;
    }
```

### Using the Database in Controllers - Basic

- Ensure that the configuration for the database is completed and working
- Endpoint methods should expect an argument of type `ApplicationDBContext` to be able to use the database
- Use the dbContext to perform database queries
  - If creating a new object, create a model first from the dto and then perform the add
- Convert updated results back to dto if the response requires data transferred back

```cs
namespace MusicStore.Controllers;

public static class SongControllers
{
    public static RouteGroupBuilder MapSongControllers(this WebApplication app)
    {
        var group = app.MapGroup("songs");

        // Get all songs
        group.MapGet("/", (ApplicationDBContext dbContext) => dbContext.Songs.ToList());

        // Create a new song
        group.MapPost("/", (CreateSongDto newSong, ApplicationDBContext dbContext) =>
        {
            Song song = new()
            {
                Name = newSong.Name,
                Genre = dbContext.Genres.Find(newSong.GenreId), // Ensure GenreId is valid
                Artist = newSong.Artist,
                Duration = newSong.Duration,
                ReleaseDate = newSong.ReleaseDate
            };

            dbContext.Songs.Add(song);
            dbContext.SaveChanges();

            SongDto songDto = new(
                song.Id,
                song.Name,
                song.Genre!.Name,
                song.Artist,
                song.Duration,
                song.ReleaseDate
            );

            return Results.CreatedAtRoute("GetSong", new { id = song.Id }, songDto);
        }).WithParameterValidation();

        // Get a specific song by id
        group.MapGet("/{id}", (int id, ApplicationDBContext dbContext) =>
        {
            Song? song = dbContext.Songs.Find(id);
            return song is null ? Results.NotFound() : Results.Ok(song);
        }).WithName("GetSong");

        // Update a song
        group.MapPut("/{id}", (int id, UpdateSongDto updatedSongDTO, ApplicationDBContext dbContext) =>
        {
            Song? song = dbContext.Songs.Find(id);

            if (song == null) return Results.NotFound();

            PropertyInfo[] updatedProperties = typeof(UpdateSongDto).GetProperties();

            foreach (PropertyInfo property in updatedProperties)
            {
                var newValue = property.GetValue(updatedSongDTO);

                PropertyInfo? songProperty = typeof(Song).GetProperty(property.Name);

                if (songProperty != null && songProperty.CanWrite)
                {
                    songProperty.SetValue(song, newValue);
                }
            }

            dbContext.SaveChanges();
            return Results.NoContent();
        }).WithParameterValidation();

        // Delete a song
        group.MapDelete("/{id}", (int id, ApplicationDBContext dbContext) =>
        {
            Song? song = dbContext.Songs.Find(id);

            if (song == null) return Results.NotFound();

            dbContext.Songs.Remove(song);
            dbContext.SaveChanges();

            return Results.NoContent();
        });

        return group;
    }
}
```

### Creating Better Endpoints - Standard

- Create A Controller For your model and inherit from `ControllerBase` using `Microsoft.AspNetCore.Mvc`
- Define the route for the controller using the `Route` by either using `controller` or by being explicit
- Use the `ApiController` attribute on the Controller, which will help with validation and help with providing better error responses

```cs
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("/api/[Controller]")] // or [Route("/api/[songs]")]
    [ApiController]
    public class SongController : ControllerBase
    {

    }
}
```

- Create a constructor that will receive the `ApplicationDBContext` instance to work with the database and create assign it to a private property

```cs
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("/api/[Controller]")] // or [Route("/api/[songs]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public SongController(ApplicationDBContext context)
        {
            _context = context;
        }
    }
}
```

- Add methods that return `IActionResult` with the attribute of `[Http<Verb>]`
- Ensure that they are async and encapsulated by a `Task`

```cs
[HttpGet]
public async Task<IActionResult> GetAll()
{
    var songs = await _context.Songs
                .Select((song) => song.ToDto())
                .ToListAsync();
    return Ok(songs)
}

[HttpGet("{id}")]
public async Task<IActionResult> GetSongById([FromRoute] int id)
{
    var song = await _context.Songs.FindAsync(id);
    if (song == null) {
        return NotFound();
    }
    return Ok(song.ToDto());
}

[HttpPost]
public async Task <IActionResult> CreateSong([FromBody] CreateSongDto newSong)
{
    Song song = newSong.ToEntity();

    await _context.Song.AddAsync(song);
    await _context.SaveChangesAsync();

    return CreatedAtAction(
        nameof(GetSongById),
        new { id = song.Id },
        song.ToDto()
    );
}

[HttpPut]
[Route("{id}")]
public async Task<IActionResult> UpdateSongById([FromRoute] int id, [FromBody] UpdateSongDto updatedSong)
{
    Song? song = await _context.Song.FindAsync(id);

    if (song is null) {
        return NotFound();
    }

    _context.Song.Entry(song).CurrentValues.SetValues(updatedSong);
    await _context.SaveChangesAsync();

    return NoContent();
}

[HttpDelete]
[Route("{id}")]
public async Task<IActionResult> DeleteSongById([FromRoute] int id)
{
    await _context.Song.Where((song) => song.Id == id).ExecuteDeleteAsync();
    await _context.SaveChangesAsync();

    return NoContent();
}
```

- Register the Controllers using the `builder`

```cs
// Program.cs

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
```

- Map controllers to the `app` before running the `app`

```cs
app.MapControllers();
app.Run();
```

## Creating Mappers

Create methods to extend models to dtos and vise versa to help maintain uniformity in the api requests and responses

- Create a dir called `Mappers`
- Create a class for each entity or model
- Create static methods that receive a dto object and return an entity object

```cs
namespace MusicStore.Mappers
{
    public static class SongMappers
    {
        public static Song ToEntity(this CreateSongDto song) {
        return new Song()
            {
                Name = song.Name,
                GenreId = song.GenreId,
                Price = song.Price,
                ReleaseDate = song.ReleaseDate
            };
        }

        public static SongDto ToDto(this Song song)
        {
            return new(
                song.Id,
                song.Name,
                song.GenreId,
                song.Price,
                song.ReleaseDate
            );
        }
    }
}
```

- Using The Mappers in the controller is as simple as called the object and calling the associated method to covert them to another object

```cs
        group.MapPost("/", async (CreateSongDto newSong, ApplicationDBContext dbContext) =>
        {
            Song song = newGame.ToEntity();

            dbContext.Songs.Add(song);
            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute("GetSong", new { id = song.Id }, song.ToDto());
        }).WithParameterValidation();
```

## Queries

- Updating objects can be done with the `Entry` method which accept an instance of a model. This can be chained to access the `CurrentValues` property which contains the `SetValues` method to make necessary changes

  ```cs
      group.MapPut("/{id}", async (int id, UpdateSongDto updatedSongDTO, ApplicationDBContext dbContext) =>
      {
          await Song? song = dbContext.Songs.Find(id);

          if (song == null) return Results.NotFound();

          dbContext.Entry(song)
              .CurrentValues
              // ToEntity method is a Mapper method which accepts an Id
              // and returns a model instance with the id property set
              .SetValues(updatedSongDTO.ToEntity(id));
          await dbContext.SaveChangesAsync();
          return Results.NoContent();
      }).WithParameterValidation();
  ```

- deleting values

  ```cs
  group.MapDelete("/{id}", async (int id, ApplicationDBContext dbContext) =>
  {
      await dbContext.Songs
          .Where((song) => song.Id == id)
          .ExecuteDeleteAsync();

      return Results.NoContent();
  });
  ```

- Creating lists of objects and converting them to Dtos
  ```cs
  group.MapGet("/", async (ApplicationDBContext dbContext) =>
      await dbContext.Songs
          // A method to get related objects
          .Include(song => song.Genre)
          // Returns songs and maps them to desired dto
          .Select((song) => song.ToSongSummaryDto())
          // to improve performance, we disregard tracking by EFC
          .AsNoTracking()
          .ToListAsync()
  );
  ```

## Repositories

- Create two directories
  - `Interfaces`
  - `Repositories`
- Create an interface that meets the controllers required queries
- Create a class for a repository to interface with the database and uses the `dbcontext`
- Ensure the class implements the interface
- Register the repository at the application level for injection as a `scoped` service
    ```cs
    builder.Services.AddScoped<ISongRepository, SongRepository>();
    ```
- Put All queries per controller in the repositories
- Ensure the controller receives an instance of the repository or replace the `ApplicationDBContext` with the `<Entity>Repository`


## Adding Authentication
- Download the following requirements
    - [Microsoft.Extensions.Identity.Core](https://www.nuget.org/packages/Microsoft.Extensions.Identity.Core/9.0.0-rc.2.24474.3)
    ```bash
    dotnet add package Microsoft.Extensions.Identity.Core --version 8.0.8
    ```
    - [Microsoft.AspNetCore.Identity.EntityFrameworkCore](https://www.nuget.org/packages/Microsoft.AspNetCore.Identity.EntityFrameworkCore/9.0.0-rc.2.24474.3)
    ```bash
    dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore --version 8.0.8
    ```
    - [Microsoft.AspNetCore.Authentication.JwtBearer](https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.JwtBearer/9.0.0-rc.2.24474.3)
    ```bash
    dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 8.0.8
    ```
- Create a model for the user extending the `Identity` package
```cs
using Microsoft.AspNetCore.Identity;

namespace MusicStore.Models
{
    public class AppUser : IdentityUser
    {
        
    }
}
```

- Update the `ApplicationDBContext` to inherit from `IdentityDbContext` and pass your user model
```cs
using Microsoft.EntityFrameworkCore;
using MusicStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MusicStore.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
        {
            
        }

        public DbSet<Artist> Artist { get; set; }
        public DbSet<Album> Album { get; set; }
        public DbSet<Song> Song { get; set; }
    }  
}
```
- Update the `Program.cs` File with the new package under the configuration where we added our sql configuration by adding
    - the `AddIdentity` method with the `AppUser` and the `IdentityRole` as types
    - Set the password configuration anmd requirements
    - Pass the `ApplicationDBContext` type as an argument for the `AddEntityFrameworkStores`

```cs
builder.Services.AddIdentity<AppUser, IdentityRole>(
    options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequiredLength = 12;
    })
    .AddEntityFrameworkStores<ApplicationDBContext>()
    .AddDefaultTokenProviders();
```

- Create and setup Authentication schemes and configuration using JWT
```cs
builder.Services.AddAuthentication(
    options =>
    {
        options.DefaultAuthenticateScheme =
        options.DefaultChallengeScheme =
        options.DefaultForbidScheme =
        options.DefaultScheme =
        options.DefaultSignInScheme =
        options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"])
            )
        };
});
```
- Update the `appsettings.json` file with the keys
```json
{
  "JWT": {
    "Issuer": "http://localhost:5246",
    "Audience": "http://localhost:5246",
    "SigningKey": "sdgfijjh3466iu345g87g08c24g7204gr803g30587ghh35807fg39074fvg80493745gf082b507807g807fgf"
  }
}
```
- Ensure the the middlewares for `UseAppAuthentication` and `UseAuthorization` are invoked on the application before the `app.MapControllers` method
```cs
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
```
- Run the migrations command and the database up
```bash
dotnet ef migrations add Identity --output-dir Data\Migrations
dotnet ef database update
```


- Modify the `ApplicationDBContext` class with the `OnModelCreating` method to seed roles for the `Identity` package
```cs
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
```


### Register users
- Create `RegisterDto` with required user data
- Create an Account Controller that receives a `UserManager` with user model and assign it to a private readonly field
```cs
using MusicStore.Dtos.Account;
using MusicStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MusicStore.Controllers
{
    [Route("/api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
    }
}
```
- create a register method
```cs
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto userData)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var appUser = new AppUser
                {
                    UserName = userData.Username,
                    Email = userData.Email
                };

                var createdUser = await _userManager.CreateAsync(appUser, userData.Password!);

                if (!createdUser.Succeeded) return StatusCode(500, createdUser.Errors);
                
                var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                
                if (!roleResult.Succeeded) return StatusCode(500, roleResult.Errors);
                
                return Ok("User Created");
            }
            catch (System.Exception e)
            {

                return StatusCode(500, e);
            }
        }
```
- Create an interface called `ITokenService` with a method called `CreateToken`
```cs
namespace MusicStore.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
```

- Create a directory called `Services` and a `TokenService` class that inherits from `ITokenService` and implement the `CreateToken` method and receives the configuration
```cs
namespace MusicStore.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config)
        {
            _config = config;
            _key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["JWT:SigningKey"])
            );
        }
        public string CreateToken(AppUser user)
        {
            throw new NotImplementedException();
        }
    }
}
```

- Create the register method
```cs
        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserName!)
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
```
- Include the `TokenService` as a scoped service
```cs
builder.Services.AddScoped<ITokenService, TokenService>();
```
- Create a dto for the return object
```cs
namespace MusicStore.Dtos.Account
{
    public class NewUserDto
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Token { get; set; }
    }
}
```
- Update the Account controller to receive the TokenService in the constructor
- Update the response the return the new user dto with the Token

### Login Users



## Uploading Files and Images

## References

- [Windows Documentation](https://learn.microsoft.com/en-us/dotnet/)
- [Nuget](https://www.nuget.org/): Search for and install packages
