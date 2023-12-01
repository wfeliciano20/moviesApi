using moviesApi.Dtos;
using moviesApi.Models;

namespace moviesApi.Repositories
{
    public interface IMovieRepository
    {
        Task<ServiceResponse<List<MovieResponseDto>>> GetAllMovies();

        Task<ServiceResponse<MovieResponseDto>> GetMovieById(int id);

        Task<ServiceResponse<MovieResponseDto>> CreateMovie(MovieDto movie);

        Task<ServiceResponse<MovieResponseDto>> UpdateMovie(int id, MovieDto movie);

        Task<ServiceResponse<MovieResponseDto>> DeleteMovie(int id);

        Task<ServiceResponse<String>> AddGenreToMovie(int id, int genreId);
    }
}
