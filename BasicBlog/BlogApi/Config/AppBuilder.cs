using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasicBlog.Repositories;
using BlogApi.Data;
using BlogApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Config
{
    public static class AppBuilder
    {
        public static void BuildAll(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            // builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            
            
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            builder.Services.AddDbContext<ApplicationDBContext>(options =>
                options.UseSqlServer(
                    builder.Configuration
                    .GetConnectionString("DefaultConnection")
                )
            );

            
        }
    }
}