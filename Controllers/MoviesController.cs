using Microsoft.AspNetCore.Mvc;
using MovieAPI.Repositories;

namespace MovieAPI.Controllers
{

    [ApiController]
    [Route("[Controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesRepository _moviesRepository;

        public MoviesController(IMoviesRepository moviesRepository)
        {
            _moviesRepository = moviesRepository;
        }

        [HttpGet]
        [Route("stats")]
        public IActionResult Get()
        {
            var stats = _moviesRepository.GetMoviesStats();
            return Ok(stats);
        }

    }
}
