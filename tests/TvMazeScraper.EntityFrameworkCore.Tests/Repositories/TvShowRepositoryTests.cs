using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using TvMazeScraper.Entities;

namespace TvMazeScraper.EntityFrameworkCore.Repositories
{
    public class TvShowRepositoryTests : EntityFrameworkCoreTestsBase
    {
        private readonly TvShowRepository _tvShowRepository;
        private readonly ITvMazeScraperUnitOfWork _uow;
        private readonly TvMazeScraperDbContext _dbContext;

        public TvShowRepositoryTests()
        {
            SeedDatabase();

            _dbContext = ServiceProvider.GetRequiredService<TvMazeScraperDbContext>();
            _uow = ServiceProvider.GetRequiredService<ITvMazeScraperUnitOfWork>();
            _tvShowRepository = (TvShowRepository)_uow.TvShows;
        }

        [Fact]
        public async Task GetAsync_Should_ReturnTvShowsInAscendingOrderOfId()
        {
            var results = await _tvShowRepository.GetAsync(0, int.MaxValue, CancellationToken.None);

            results.Should().HaveCount(10);
            results.Select(s => s.Id).Should().BeInAscendingOrder();
        }

        [Fact]
        public async Task GetAsync_Should_Return2TvShowsWithTakeAndSkip()
        {
            var results = await _tvShowRepository.GetAsync(1, 2, CancellationToken.None);

            results.Should().HaveCount(2);
            results.First().Name.Should().Be("TV Show 2");
            results.Last().Name.Should().Be("TV Show 3");
        }

        [Fact]
        public async Task GetAsync_Should_ReturnTvShowsWithActorsInAscendingOrderOfBirthday()
        {
            var results = await _tvShowRepository.GetAsync(0, int.MaxValue, CancellationToken.None);

            results.Should().HaveCount(10);
            results.First().Cast.Should().HaveCount(3);
            results.First().Cast.Select(a => a.Birthday).Should().BeInDescendingOrder();
        }

        private void SeedDatabase()
        {
            var context = ServiceProvider.GetRequiredService<TvMazeScraperDbContext>();

            var actorA = new Actor { Name = "Actor A", Birthday = new DateOnly(1980, 5, 12) };
            context.Actors.Add(actorA);
            var actorB = new Actor { Name = "Actor B", Birthday = new DateOnly(1970, 5, 12) };
            context.Actors.Add(actorB);
            var actorC = new Actor { Name = "Actor A", Birthday = new DateOnly(1990, 5, 12) };
            context.Actors.Add(actorC);
            context.SaveChanges();

            context.TvShows.Add(new TvShow() { Id = 1, Name = "TV Show 1", Cast = new List<Actor> { actorA, actorB, actorC } });
            context.TvShows.Add(new TvShow() { Id = 2, Name = "TV Show 2", Cast = new List<Actor> { actorA, actorB, actorC } });
            context.TvShows.Add(new TvShow() { Id = 3, Name = "TV Show 3", Cast = new List<Actor> { actorA, actorB, actorC } });
            context.TvShows.Add(new TvShow() { Id = 4, Name = "TV Show 4", Cast = new List<Actor> { actorA, actorB, actorC } });
            context.TvShows.Add(new TvShow() { Id = 5, Name = "TV Show 5", Cast = new List<Actor> { actorA, actorB, actorC } });
            context.TvShows.Add(new TvShow() { Id = 6, Name = "TV Show 6", Cast = new List<Actor> { actorA, actorB, actorC } });
            context.TvShows.Add(new TvShow() { Id = 7, Name = "TV Show 7", Cast = new List<Actor> { actorA, actorB, actorC } });
            context.TvShows.Add(new TvShow() { Id = 8, Name = "TV Show 8", Cast = new List<Actor> { actorA, actorB, actorC } });
            context.TvShows.Add(new TvShow() { Id = 9, Name = "TV Show 9", Cast = new List<Actor> { actorA, actorB, actorC } });
            context.TvShows.Add(new TvShow() { Id = 10, Name = "TV Show 10", Cast = new List<Actor> { actorA, actorB, actorC } });
            context.SaveChanges();
        }
    }
}
