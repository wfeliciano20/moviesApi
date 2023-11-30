using moviesApi.Dtos;
using moviesApi.Models;

namespace moviesApi.Repositories
{
    public interface IMovieRepository
    {
        Task<ServiceResponse<List<Movie>>> GetAllMovies();

        Task<ServiceResponse<Movie>> GetMovieById(int id);

        Task<ServiceResponse<Movie>> CreateMovie(MovieDto movie);

        Task<ServiceResponse<Movie>> UpdateMovie(int id, MovieDto movie);

        Task<ServiceResponse<Movie>> DeleteMovie(int id);

        Task<ServiceResponse<String>> AddGenreToMovie(int id, int genreId);
    }
}
