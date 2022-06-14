using MediatR;
using Microsoft.Extensions.Options;
using TvMazeScraper.Models;

namespace TvMazeScraper.TvShows
{
    public class GetPagedTvShowListQuery : IRequest<TvShowListModel>
    {
        public int Page { get; set; }

        public int? ItemsPerPage { get; set; }

        public class Handler : IRequestHandler<GetPagedTvShowListQuery, TvShowListModel>
        {
            private readonly IOptionsSnapshot<TvMazeScraperOptions> _options;
            private readonly ITvMazeScraperUnitOfWork _uow;

            public Handler(ITvMazeScraperUnitOfWork uow, IOptionsSnapshot<TvMazeScraperOptions> options)
            {
                _options = options;
                _uow = uow;
            }

            public async Task<TvShowListModel> Handle(GetPagedTvShowListQuery request, CancellationToken cancellationToken)
            {
                var model = new TvShowListModel
                {
                    Page = request.Page,
                    ItemsPerPage = request.ItemsPerPage ?? _options.Value.ItemsPerPage,
                    Total = await _uow.TvShows.CountAsync(cancellationToken)
                };

                var skip = (request.Page) * model.ItemsPerPage;
                var tvShows = await _uow.TvShows.GetAsync(skip, model.ItemsPerPage, cancellationToken);
                model.TvShows = tvShows.Select(s => new TvShowModel
                {
                    Id = s.Id,
                    TvMazeId = s.Id,
                    Name = s.Name,
                    Cast = s.Cast.Select(a => new ActorModel { Id = a.Id, Name = a.Name, Birthday = a.Birthday })
                });

                return model;
            }
        }
    }
}
