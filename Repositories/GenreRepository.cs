using AutoMapper;
using Microsoft.EntityFrameworkCore;
using moviesApi.Data;
using moviesApi.Dtos;
using moviesApi.Models;

namespace moviesApi.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GenreRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<string>> AddMovieToGenre(int id, int movieId)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            var dbGenre = await _context.Genres.FirstOrDefaultAsync(g=>g.Id == id);
            if(dbGenre == null) 
            {
                var dbMovie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == movieId);
                if( dbMovie == null )
                {
                    if(dbGenre.Movies != null)
                    {
                        dbGenre.Movies.Add(dbMovie);
                        await _context.SaveChangesAsync();
                        response.Data = $"Successfully added Movie with id:{id} to Genre with id:{movieId}";
                        return response;
                    }
                    else
                    {
                        dbGenre.Movies = new List<Movie>();
                        dbGenre.Movies.Add(dbMovie);
                        await _context.SaveChangesAsync();
                        response.Data = $"Successfully added Movie with id:{id} to Genre with id:{movieId}";
                        return response;
                    }
                }
                else
                {
                    response.Success = false;
                    response.Message = $"Movie with id:{movieId} not found.";
                    response.ResponseCode = System.Net.HttpStatusCode.NotFound;
                    return response;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"Genre with id:{id} not found.";
                response.ResponseCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }
        }

        public async Task<ServiceResponse<Genre>> CreateGenre(GenreDto genre)
        {
            ServiceResponse<Genre> response = new ServiceResponse<Genre>();
            var genreToSave = _mapper.Map<Genre>(genre);
            if(await _context.Genres.AnyAsync(g=>g.Name ==genre.Name))
            {
                response.Success = false;
                response.Message = "Genre Already Exists.";
                response.ResponseCode = System.Net.HttpStatusCode.BadRequest;
                return response;
            }
            _context.Genres.Add(genreToSave);
            await _context.SaveChangesAsync();
            response.Data = genreToSave;
            return response;
        }

        public async Task<ServiceResponse<Genre>> DeleteGenre(int id)
        {
            ServiceResponse<Genre> response = new ServiceResponse<Genre>();
            var genreToDelete = await _context.Genres.FirstOrDefaultAsync(g => g.Id == id);
            if (genreToDelete != null)
            {
                _context.Genres.Remove(genreToDelete);
                await _context.SaveChangesAsync();
                response.Data = genreToDelete;
                return response;
              
            }
            response.Success = false;
            response.Message = "Genre Not Found.";
            response.ResponseCode = System.Net.HttpStatusCode.NotFound;
            return response;
        }

        public async Task<ServiceResponse<List<Genre>>> GetAllGenres()
        {
            ServiceResponse<List<Genre>> response = new ServiceResponse<List<Genre>>();

            var genres = await _context.Genres.Include(g=>g.Movies).ToListAsync();
            response.Data = genres;
            return response;

        }

        public async Task<ServiceResponse<Genre>> GetGenreById(int id)
        {
            ServiceResponse<Genre> response = new ServiceResponse<Genre>();
            var dbGenre = await _context.Genres.Include(g=>g.Movies).FirstOrDefaultAsync(g => g.Id == id);
            if(dbGenre != null)
            {
                response.Data = dbGenre;
                return response;
            }
            response.Success = false;
            response.Message = $"Genre with id:{id} not found.";
            response.ResponseCode= System.Net.HttpStatusCode.NotFound;
            return response;
        }

        public async Task<ServiceResponse<Genre>> UpdateGenre(int id, GenreDto genre)
        {
            ServiceResponse<Genre> response = new ServiceResponse<Genre>();
            var genreToUpdate = await _context.Genres.FirstOrDefaultAsync(g =>g.Id == id);
            if(genreToUpdate != null) 
            {
                genreToUpdate = _mapper.Map<Genre>(genre);
                response.Data=genreToUpdate;
                return response;
            }

            response.Success=false;
            response.Message = $"Genre with id:{id} not found.";
            response.ResponseCode = System.Net.HttpStatusCode.NotFound;
            return response;
        }
    }
}
