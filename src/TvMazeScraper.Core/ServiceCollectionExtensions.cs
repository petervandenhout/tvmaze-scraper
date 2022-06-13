using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TvMazeScraper
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTvMazeScraperCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<TvMazeScraperOptions>(configuration.GetSection(nameof(TvMazeScraperOptions)));

            return services;
        }
    }
}
