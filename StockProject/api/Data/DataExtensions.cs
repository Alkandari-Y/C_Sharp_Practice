using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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