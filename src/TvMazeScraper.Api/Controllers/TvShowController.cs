using MediatR;
using Microsoft.AspNetCore.Mvc;
using TvMazeScraper.TvShows;

namespace TvMazeScraper.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TvShowController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<TvShowController> _logger;

        public TvShowController(IMediator mediator, ILogger<TvShowController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("scrape")]
        public async Task<IActionResult> Scrape(CancellationToken cancellationToken)
        {
            await _mediator.Send(new ScrapeTvMazeApiCommand(), cancellationToken);

            return Ok("We are now scraping the TVMaze api. This might take a while. The TV shows will be added to our database. You will not be updated about the progress.");
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken, [FromQuery] int page = 0, [FromQuery] int? itemsPerPage = null)
        {
            var result = await _mediator.Send(new GetPagedTvShowListQuery { Page = page, ItemsPerPage = itemsPerPage }, cancellationToken);

            return Ok(result);
        }
    }
}