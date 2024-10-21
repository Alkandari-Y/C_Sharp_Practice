using BlogApi.Config;

var builder = WebApplication.CreateBuilder(args);

builder.BuildAll();

var app = builder.Build();

await app.RunMiddlewaresAsync();

app.Run();
