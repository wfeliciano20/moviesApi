using Microsoft.AspNetCore.Mvc;
using moviesApi.Dtos;
using moviesApi.Models;
using moviesApi.Repositories;

namespace moviesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

        public MoviesController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<MovieResponseDto>>>> GetAlLMovies()
        {
            return Ok(await _movieRepository.GetAllMovies());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<MovieResponseDto>>> GetMovieById(int id)
        {
            return Ok(await _movieRepository.GetMovieById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<MovieResponseDto>>> CreateMovie(MovieDto movieDto)
        {
            return Ok(await _movieRepository.CreateMovie(movieDto));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<MovieResponseDto>>> UpdateMovie(int id, MovieDto movieDto)
        {
            return Ok(await _movieRepository.UpdateMovie(id, movieDto));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<MovieResponseDto>>> DeleteMovie(int id)
        {
            return Ok(await _movieRepository.DeleteMovie(id));
        }

        [HttpPost("{id}/{gId}")]
        public async Task<ActionResult<ServiceResponse<string>>> AddGenreToMovie(int id,int gId)
        {
            return Ok(await _movieRepository.AddGenreToMovie(id, gId));
        }
    }
}
