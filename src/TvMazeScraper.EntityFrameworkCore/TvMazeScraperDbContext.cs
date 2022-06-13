using Microsoft.EntityFrameworkCore;
using TvMazeScraper.Entities;

namespace TvMazeScraper.EntityFrameworkCore
{
    public class TvMazeScraperDbContext : DbContext
    {
        public DbSet<TvShow> TvShows { get; set; }

        public DbSet<Actor> Actors { get; set; }

        public TvMazeScraperDbContext(DbContextOptions<TvMazeScraperDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}