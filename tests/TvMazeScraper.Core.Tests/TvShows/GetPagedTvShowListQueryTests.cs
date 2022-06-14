using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvMazeScraper.Entities;

namespace TvMazeScraper.TvShows
{
    public class GetPagedTvShowListQueryTests : MediatRTestsBase
    {
        public GetPagedTvShowListQueryTests() : base()
        {
            var tvShows = new List<TvShow>();
            var actorA = new Actor { Name = "Actor A", Birthday = new DateTime(1980, 5, 12) };
            var actorB = new Actor { Name = "Actor B", Birthday = new DateTime(1970, 5, 12) };
            var actorC = new Actor { Name = "Actor A", Birthday = new DateTime(1990, 5, 12) };

            tvShows.Add(new TvShow() { Id = 1, Name = "TV Show 1", Cast = new List<Actor> { actorA, actorB, actorC } });
            tvShows.Add(new TvShow() { Id = 2, Name = "TV Show 2", Cast = new List<Actor> { actorA, actorB, actorC } });
            tvShows.Add(new TvShow() { Id = 3, Name = "TV Show 3", Cast = new List<Actor> { actorA, actorB, actorC } });
            tvShows.Add(new TvShow() { Id = 4, Name = "TV Show 4", Cast = new List<Actor> { actorA, actorB, actorC } });
            tvShows.Add(new TvShow() { Id = 5, Name = "TV Show 5", Cast = new List<Actor> { actorA, actorB, actorC } });
            tvShows.Add(new TvShow() { Id = 6, Name = "TV Show 6", Cast = new List<Actor> { actorA, actorB, actorC } });
            tvShows.Add(new TvShow() { Id = 7, Name = "TV Show 7", Cast = new List<Actor> { actorA, actorB, actorC } });
            tvShows.Add(new TvShow() { Id = 8, Name = "TV Show 8", Cast = new List<Actor> { actorA, actorB, actorC } });
            tvShows.Add(new TvShow() { Id = 9, Name = "TV Show 9", Cast = new List<Actor> { actorA, actorB, actorC } });
            tvShows.Add(new TvShow() { Id = 10, Name = "TV Show 10", Cast = new List<Actor> { actorA, actorB, actorC } });

            TvShowRepositoryMock.Setup(m => m.GetAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(tvShows);
            TvShowRepositoryMock.Setup(m => m.CountAsync(It.IsAny<CancellationToken>())).ReturnsAsync(tvShows.Count);
        }

        [Fact]
        public async Task Handler_Should_GetTvShows_ForPage0()
        {
            var result = await Mediator.Send(new GetPagedTvShowListQuery { Page = 0 });

            result.Should().NotBeNull();

            TvShowRepositoryMock.Verify(m => m.GetAsync(0, It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handler_Should_GetTvShows_ForPage0_WithItemsPerPage2()
        {
            var result = await Mediator.Send(new GetPagedTvShowListQuery { Page = 0, ItemsPerPage = 2 });

            result.Should().NotBeNull();

            TvShowRepositoryMock.Verify(m => m.GetAsync(0, 2, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handler_Should_GetTvShows_ForPage2()
        {
            var result = await Mediator.Send(new GetPagedTvShowListQuery { Page = 2, ItemsPerPage = 2 });

            result.Should().NotBeNull();

            TvShowRepositoryMock.Verify(m => m.GetAsync(4, 2, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handler_Should_ReturnTvShowListModel_WithCast()
        {
            var result = await Mediator.Send(new GetPagedTvShowListQuery());

            result.Should().NotBeNull();
            result.TvShows.First().Cast.Should().HaveCount(3);
        }
    }
}
