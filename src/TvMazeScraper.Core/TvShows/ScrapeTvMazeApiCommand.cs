using MediatR;
using System.Globalization;
using TvMazeScraper.Entities;

namespace TvMazeScraper.TvShows
{
    public class ScrapeTvMazeApiCommand : IRequest
    {
        public class Handler : AsyncRequestHandler<ScrapeTvMazeApiCommand>
        {
            private readonly ITvMazeScraperUnitOfWork _uow;
            private readonly IMediator _mediator;

            public Handler(ITvMazeScraperUnitOfWork uow, IMediator mediator)
            {
                _uow = uow;
                _mediator = mediator;
            }

            protected override async Task Handle(ScrapeTvMazeApiCommand request, CancellationToken cancellationToken)
            {
                var hasMoreShows = true;
                var page = 0;
                while (hasMoreShows)
                {
                    var shows = await _mediator.Send(new GetTvMazeShowsCommand { Page = page }, cancellationToken);

                    page++;
                    if (shows.Count() == 0)
                        hasMoreShows = false;

                    foreach (var show in shows)
                    {
                        var cast = await _mediator.Send(new GetTvMazeCastForShowCommand { ShowId = show.Id }, cancellationToken);

                        var tvShow = new TvShow
                        {
                            TvMazeId = show.Id,
                            Name = show.Name,
                            Cast = new List<Actor>()
                        };

                        // Note: actors will not be unique in the database
                        foreach (var actor in cast)
                        {
                            DateTime? birthday = null;
                            if (DateTime.TryParseExact(actor.Person.Birthday, "yyyy-mm-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var b))
                            {
                                birthday = b;
                            }

                            tvShow.Cast.Add(new Actor { Name = actor.Person.Name, Birthday = birthday });
                        }

                        _uow.TvShows.Add(tvShow);
                        await _uow.SaveChangesAsync(cancellationToken);
                    }
                }
            }
        }
    }
}
