using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Models;
using Microsoft.EntityFrameworkCore;



namespace GameStore.Data
{
    public class ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : DbContext(options)
    {
        public DbSet<Game> Games => Set<Game>();
        public DbSet<Genre> Genres => Set<Genre>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>().HasData(
                new { Id = 1, Name = "Fighting" },
                new { Id = 2, Name = "Action" },
                new { Id = 3, Name = "Puzzle" },
                new { Id = 4, Name = "MOBA" },
                new { Id = 5, Name = "RPG" },
                new { Id = 6, Name = "Simulation" },
                new { Id = 7, Name = "Stealth" }
            );
        }
    }
}

