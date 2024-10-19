using GameStore.Controllers;
using GameStore.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSqlite<ApplicationDBContext>(
    builder.Configuration.GetConnectionString("DefaultConnection")
);

var app = builder.Build();

app.MapGameControllers();
app.MapGenreControllers();
await app.MigrateDbAsync();

app.Run();



