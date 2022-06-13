using Microsoft.AspNetCore.Mvc;

namespace TvMazeScraper.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TvShowController : ControllerBase
    {
        private readonly ILogger<TvShowController> _logger;

        public TvShowController(ILogger<TvShowController> logger)
        {
            _logger = logger;
        }
    }
}