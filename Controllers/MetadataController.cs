using Microsoft.AspNetCore.Mvc;
using MovieAPI.Data.Models;
using MovieAPI.Repositories;
using System.Collections.Generic;

namespace MovieAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class MetadataController : ControllerBase
    {
        private readonly IMoviesRepository _moviesRepository;

        public MetadataController(IMoviesRepository moviesRepository)
        {
            _moviesRepository = moviesRepository;
        }

        [HttpPost]
        public IActionResult Post(Metadata metadata)
        {
            var result = _moviesRepository.AddMetadata(metadata);
            if (result)
                return Ok();
            return BadRequest();
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult Get(int id)
        {
            List<Metadata> metadata = _moviesRepository.GetMovieMetadaById(id);
            if(metadata.Count > 0)
                return Ok(metadata);
            return NotFound();
        }


    }
}
