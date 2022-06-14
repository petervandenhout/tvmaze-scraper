using MediatR;
using System.Text.Json;
using TvMazeScraper.Models;

namespace TvMazeScraper.TvShows
{
    public class GetTvMazeCastForShowCommand : IRequest<IEnumerable<TvMazeCastModel>>
    {
        public int ShowId { get; set; }

        public class Handler : IRequestHandler<GetTvMazeCastForShowCommand, IEnumerable<TvMazeCastModel>>
        {
            private readonly IHttpClientFactory _httpClientFactory;

            public Handler(IHttpClientFactory httpClientFactory)
            {
                _httpClientFactory = httpClientFactory;
            }

            public async Task<IEnumerable<TvMazeCastModel>> Handle(GetTvMazeCastForShowCommand request, CancellationToken cancellationToken)
            {
                // wait .5 seconds to not trigger the ratelimit for the TvMaze api
                Thread.Sleep(500);

                using var httpClient = _httpClientFactory.CreateClient();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, $"https://api.tvmaze.com/shows/{request.ShowId}/cast");

                var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

                IEnumerable<TvMazeCastModel>? cast = null;
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                    cast = await JsonSerializer.DeserializeAsync<IEnumerable<TvMazeCastModel>>(contentStream, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                }

                if (cast != null)
                    return cast;

                return new List<TvMazeCastModel>();
            }
        }
    }
}
