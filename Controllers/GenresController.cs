using Microsoft.AspNetCore.Mvc;
using moviesApi.Dtos;
using moviesApi.Models;
using moviesApi.Repositories;

namespace moviesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController : ControllerBase
    {
        private readonly IGenreRepository _genreRepository;

        public GenresController(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Genre>>>> GetAlLGenres()
        {
            return Ok(await _genreRepository.GetAllGenres());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<Genre>>> GetGenreById(int id)
        {
            return Ok(await _genreRepository.GetGenreById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Genre>>> CreateGenre(GenreDto genreDto)
        {
            return Ok(await _genreRepository.CreateGenre(genreDto));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<Genre>>> UpdateGenre(int id, GenreDto genreDto)
        {
            return Ok(await _genreRepository.UpdateGenre(id,genreDto));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<Genre>>> DeleteGenre(int id)
        {
            return Ok(await _genreRepository.DeleteGenre(id));
        }

        [HttpPost("{id}/{mId}")]
        public async Task<ActionResult<ServiceResponse<string>>> AddMovieToGenre(int id, int mId)
        {
            return Ok(await _genreRepository.AddMovieToGenre(id,mId));
        }
    }
}
