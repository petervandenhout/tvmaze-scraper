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

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken, [FromQuery] int page = 1, [FromQuery] int? itemsPerPage = null)
        {
            var result = await _mediator.Send(new GetPagedTvShowListQuery { Page = page, ItemsPerPage = itemsPerPage }, cancellationToken);

            return Ok(result);
        }
    }
}