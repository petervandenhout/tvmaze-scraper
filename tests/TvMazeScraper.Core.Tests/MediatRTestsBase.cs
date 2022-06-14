using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using TvMazeScraper.Repositories;
using TvMazeScraper.TvShows;

namespace TvMazeScraper
{
    public class MediatRTestsBase
    {
        protected readonly IServiceCollection Services;

        protected IMediator Mediator => ServiceProvider.GetRequiredService<IMediator>();

        protected IServiceProvider ServiceProvider => Services.BuildServiceProvider();

        protected Mock<ITvShowRepository> TvShowRepositoryMock { get; }

        protected Mock<ITvMazeScraperUnitOfWork> TvMazeScraperUnitOfWorkMock { get; }

        public MediatRTestsBase()
        {
            var configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .Build();

            Services = new ServiceCollection();
            Services.AddMediatR(typeof(GetPagedTvShowListQuery).Assembly);
            Services.AddTvMazeScraperCore(configuration);

            TvShowRepositoryMock = new Mock<ITvShowRepository>();

            TvMazeScraperUnitOfWorkMock = new Mock<ITvMazeScraperUnitOfWork>();
            TvMazeScraperUnitOfWorkMock.SetupGet(m => m.TvShows).Returns(TvShowRepositoryMock.Object);

            TvMazeScraperUnitOfWorkMock.Setup(m => m.SaveChanges()).Returns(1);
            TvMazeScraperUnitOfWorkMock.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            Services.AddTransient((_) => TvMazeScraperUnitOfWorkMock.Object);
        }
    }
}
