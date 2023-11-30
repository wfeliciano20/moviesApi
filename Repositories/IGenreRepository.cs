using moviesApi.Dtos;
using moviesApi.Models;

namespace moviesApi.Repositories
{
    public interface IGenreRepository
    {
        Task<ServiceResponse<List<Genre>>> GetAllGenres();

        Task<ServiceResponse<Genre>> GetGenreById(int id);

        Task<ServiceResponse<Genre>> CreateGenre(GenreDto genre);

        Task<ServiceResponse<Genre>> UpdateGenre(int id, GenreDto genre);

        Task<ServiceResponse<Genre>> DeleteGenre(int id);
        Task<ServiceResponse<String>> AddMovieToGenre(int id, int movieId);
    }
}
