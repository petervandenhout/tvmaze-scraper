using MediatR;
using Microsoft.Extensions.Options;
using TvMazeScraper.Models;

namespace TvMazeScraper.TvShows
{
    public class GetPagedTvShowListQuery : IRequest<TvShowListModel>
    {
        public int Page { get; set; } = 1;

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

                var skip = (request.Page - 1) * model.ItemsPerPage;
                model.TvShows = await _uow.TvShows.GetAsync(skip, model.ItemsPerPage, cancellationToken);

                return model;
            }
        }
    }
}
