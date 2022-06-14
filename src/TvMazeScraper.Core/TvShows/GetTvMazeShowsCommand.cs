using MediatR;
using System.Text.Json;
using TvMazeScraper.Models;

namespace TvMazeScraper.TvShows
{
    public class GetTvMazeShowsCommand : IRequest<IEnumerable<TvMazeShowModel>>
    {
        public int Page { get; set; }

        public class Handler : IRequestHandler<GetTvMazeShowsCommand, IEnumerable<TvMazeShowModel>>
        {
            private readonly IHttpClientFactory _httpClientFactory;

            public Handler(IHttpClientFactory httpClientFactory)
            {
                _httpClientFactory = httpClientFactory;
            }

            public async Task<IEnumerable<TvMazeShowModel>> Handle(GetTvMazeShowsCommand request, CancellationToken cancellationToken)
            {
                // wait .5 seconds to not trigger the ratelimit for the TvMaze api
                Thread.Sleep(500);

                using var httpClient = _httpClientFactory.CreateClient();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "https://api.tvmaze.com/shows?page=" + request.Page);

                var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

                IEnumerable<TvMazeShowModel>? tvMazeShows = null;
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                    tvMazeShows = await JsonSerializer.DeserializeAsync<IEnumerable<TvMazeShowModel>>(contentStream, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                }

                if (tvMazeShows != null)
                    return tvMazeShows;

                return new List<TvMazeShowModel>();
            }
        }
    }
}
