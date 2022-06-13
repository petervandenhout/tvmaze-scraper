using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TvMazeScraper.EntityFrameworkCore
{
    public abstract class EntityFrameworkCoreTestsBase
    {
        protected readonly IConfiguration Configuration;
        protected readonly IServiceCollection Services;

        protected IServiceProvider ServiceProvider => Services.BuildServiceProvider();

        public EntityFrameworkCoreTestsBase()
        {
            Configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .Build();

            // create a new in-memory database for each test
            Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value = $"Server=inMemory; Database=tvmaze-scraper-db-{Guid.NewGuid()};";

            Services = new ServiceCollection();
            Services.AddTvMazeScraperCore(Configuration);
            Services.AddTvMazeScraperEntityFrameworkCore(Configuration);
        }
    }
}
