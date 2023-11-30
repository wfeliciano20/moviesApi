using AutoMapper;
using Microsoft.EntityFrameworkCore;
using moviesApi.Data;
using moviesApi.Dtos;
using moviesApi.Models;
using System.Net;

namespace moviesApi.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public MovieRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<string>> AddGenreToMovie(int id, int genreId)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            var dbMovie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
            if (dbMovie == null)
            {
                var dbGenre = await _context.Genres.FirstOrDefaultAsync(g => g.Id == genreId);
                if (dbGenre == null)
                {
                    if (dbMovie.Genres != null)
                    {
                        dbMovie.Genres.Add(dbGenre);
                        await _context.SaveChangesAsync();
                        response.Data = $"Successfull added genre with id{genreId} to movie with id:{id}.";
                        return response;
                    }
                    else
                    {
                        dbMovie.Genres = new List<Genre>();
                        dbMovie.Genres.Add(dbGenre);
                        await _context.SaveChangesAsync();
                        response.Data = $"Successfull added genre with id{genreId} to movie with id:{id}.";
                        return response;
                    }
                }
                else
                {
                    response.Success = false;
                    response.Message = $"Genre with id:{genreId} was not found.";
                    response.ResponseCode = HttpStatusCode.NotFound;
                    return response;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"Movie with id:{id} was not found.";
                response.ResponseCode = HttpStatusCode.NotFound;
                return response;
            }
        }

        public async Task<ServiceResponse<Movie>> CreateMovie(MovieDto movie)
        {
            ServiceResponse<Movie> response = new ServiceResponse<Movie>();
            if (await _context.Movies.AnyAsync(m => m.Title.Equals(movie.Title)))
            {
                response.Success = false;
                response.Message = "Movie title already present in db";
                response.ResponseCode = HttpStatusCode.BadRequest;
                return response;
            }
            else
            {
                Movie movieToSave = _mapper.Map<Movie>(movie);
                movieToSave.Created = DateTime.Now;
                movieToSave.Updated = DateTime.Now;
                _context.Movies.Add(movieToSave);
                await _context.SaveChangesAsync();
                response.Data = movieToSave;
                return response;
            }
            
        }

        public async Task<ServiceResponse<Movie>> DeleteMovie(int id)
        {
            ServiceResponse<Movie> response = new ServiceResponse<Movie>();
            if(await _context.Movies.AnyAsync(m => m.Id == id))
            {
                var movieToDelete = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
                _context.Movies.Remove(movieToDelete);
                await _context.SaveChangesAsync();
                response.Data =movieToDelete;
                return response;
                
            }
            else
            {
                response.Success =false;
                response.Message = "Movie Not Found";
                response.ResponseCode = HttpStatusCode.NotFound;
                return response;
            }
        }

        public async Task<ServiceResponse<List<Movie>>> GetAllMovies()
        {
            ServiceResponse<List<Movie>> response = new ServiceResponse<List<Movie>>();
            var movies = await _context.Movies.Include(m=>m.Genres).ToListAsync();
            if(movies != null)
            {
                response.Data = movies;
                return response;
            }
            response.Success = false;
            response.Message = "No movies on Database.";
            response.ResponseCode = HttpStatusCode.NotFound;
            return response;
        }

        public async Task<ServiceResponse<Movie>> GetMovieById(int id)
        {
            ServiceResponse<Movie> response = new ServiceResponse<Movie>();
            var dbMovie = await _context.Movies.Include(m => m.Genres).FirstOrDefaultAsync(m => m.Id==id);
            if(dbMovie != null)
            {
                response.Data=dbMovie;
                return response;
            }
            else
            {
                response.Success = false;
                response.Message = $"Movie with id {id} is not on DB";
                response.ResponseCode = HttpStatusCode.NotFound;
                return response;
            }
        }

        public async Task<ServiceResponse<Movie>> UpdateMovie(int id, MovieDto movie)
        {
            ServiceResponse<Movie> response = new ServiceResponse<Movie>();
            var movieToUpdate = await _context.Movies.FirstOrDefaultAsync(m => m.Id==id);
            if (movieToUpdate != null)
            {
                movieToUpdate = _mapper.Map<Movie>(movie);
                movieToUpdate.Updated = DateTime.Now;
                await _context.SaveChangesAsync();
                response.Data = movieToUpdate;
                return response;

            }
            else
            {
                response.Success = false;
                response.Message = $"Movie with id: {id} is not on DB.";
                response.ResponseCode = HttpStatusCode.NotFound;
                return response;
            }
        }

       
    }
}
