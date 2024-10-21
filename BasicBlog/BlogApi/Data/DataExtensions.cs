using Microsoft.EntityFrameworkCore;

using BlogApi.Data;

namespace api.Data
{
    public static class DataExtensions
    {
     public static async Task MigrateAsync(this WebApplication app)
     {
            var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<ApplicationDBContext>();
            if (dbContext == null) return;

            await dbContext.Database.MigrateAsync();
     }   

    }
}