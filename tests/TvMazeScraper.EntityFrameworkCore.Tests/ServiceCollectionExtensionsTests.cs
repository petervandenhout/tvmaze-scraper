using Microsoft.Extensions.DependencyInjection;
using FluentAssertions;

namespace TvMazeScraper.EntityFrameworkCore
{
    public class ServiceCollectionExtensionsTests : EntityFrameworkCoreTestsBase
    {
        [Fact]
        public void AddTvMazeScraperEntityFrameworkCore_Should_RegisterTvMazeScraperDbContext()
        {
            var context = ServiceProvider.GetService<TvMazeScraperDbContext>();

            context.Should().NotBeNull();
        }
    }
}