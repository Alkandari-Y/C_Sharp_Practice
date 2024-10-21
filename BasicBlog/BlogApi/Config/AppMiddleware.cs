using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;

namespace BlogApi.Config
{
    public static class AppMiddleware
    {
        public static async Task  RunMiddlewaresAsync(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowAllOrigins");
            // app.UseHttpsRedirection();

            await app.MigrateAsync();

        }
    }
}