using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace TvMazeScraper.EntityFrameworkCore
{
    public static class ServiceCollectionExtensions
    {
        private static readonly InMemoryDatabaseRoot _inMemoryDatabaseRoot = new InMemoryDatabaseRoot();

        public static IServiceCollection AddTvMazeScraperEntityFrameworkCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<TvMazeScraperDbContext>((provider, builder) =>
            {
                var tvMazeScraperOptions = provider.GetRequiredService<IOptions<TvMazeScraperOptions>>();
                var connectionString = configuration.GetConnectionString(tvMazeScraperOptions.Value.ConnectionStringName);

                if (connectionString.IndexOf("inMemory", StringComparison.InvariantCultureIgnoreCase) >= 0)
                {
                    // don't raise the error warning, the in memory db doesn't support transactions
                    builder.ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));

                    var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
                    var dbName = connectionStringBuilder.InitialCatalog;

                    builder.UseInMemoryDatabase(dbName, _inMemoryDatabaseRoot);
                }
                else
                {
                    builder.UseSqlServer(connectionString);
                }
            });

            services.AddTransient<ITvMazeScraperUnitOfWork, TvMazeScraperUnitOfWork>();

            return services;
        }
    }
}
