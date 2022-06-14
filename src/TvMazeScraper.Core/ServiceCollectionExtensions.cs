using MediatR;
using MediatR.Registration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TvMazeScraper.MediatR;
using TvMazeScraper.TvShows;

namespace TvMazeScraper
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTvMazeScraperCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<TvMazeScraperOptions>(configuration.GetSection(nameof(TvMazeScraperOptions)));

            if (services.BuildServiceProvider().GetService<IMediator>() == null)
                services.AddMediatR(typeof(GetPagedTvShowListQuery).Assembly);
            ServiceRegistrar.AddMediatRClasses(services, new[] { typeof(GetPagedTvShowListQuery).Assembly });

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            return services;
        }
    }
}
