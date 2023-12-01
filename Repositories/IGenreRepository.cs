using moviesApi.Dtos;
using moviesApi.Models;

namespace moviesApi.Repositories
{
    public interface IGenreRepository
    {
        Task<ServiceResponse<List<GenreResponseDto>>> GetAllGenres();

        Task<ServiceResponse<GenreResponseDto>> GetGenreById(int id);

        Task<ServiceResponse<GenreResponseDto>> CreateGenre(GenreDto genre);

        Task<ServiceResponse<GenreResponseDto>> UpdateGenre(int id, GenreDto genre);

        Task<ServiceResponse<GenreResponseDto>> DeleteGenre(int id);
        Task<ServiceResponse<String>> AddMovieToGenre(int id, int movieId);
    }
}
