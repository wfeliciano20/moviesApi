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
        public async Task<ActionResult<ServiceResponse<List<GenreResponseDto>>>> GetAlLGenres()
        {
            return Ok(await _genreRepository.GetAllGenres());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GenreResponseDto>>> GetGenreById(int id)
        {
            return Ok(await _genreRepository.GetGenreById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GenreResponseDto>>> CreateGenre(GenreDto genreDto)
        {
            return Ok(await _genreRepository.CreateGenre(genreDto));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<GenreResponseDto>>> Update(int id, GenreDto genreDto)
        {
            return Ok(await _genreRepository.UpdateGenre(id,genreDto));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GenreResponseDto>>> DeleteGenre(int id)
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
