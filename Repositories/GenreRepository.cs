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
            if(dbGenre != null) 
            {
                var dbMovie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == movieId);
                if( dbMovie != null )
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

        public async Task<ServiceResponse<GenreResponseDto>> CreateGenre(GenreDto genre)
        {
            ServiceResponse<GenreResponseDto> response = new ServiceResponse<GenreResponseDto>();
             Genre genreToSave = new Genre 
             { 
                 Name = genre.Name,
                 Movies = new List<Movie>()
             };
            if(await _context.Genres.AnyAsync(g=>g.Name ==genre.Name))
            {
                response.Success = false;
                response.Message = "Genre Already Exists.";
                response.ResponseCode = System.Net.HttpStatusCode.BadRequest;
                return response;
            }
            _context.Genres.Add(genreToSave);
            await _context.SaveChangesAsync();
            response.Data = _mapper.Map<GenreResponseDto>(genreToSave);
            return response;
        }

        public async Task<ServiceResponse<GenreResponseDto>> DeleteGenre(int id)
        {
            ServiceResponse<GenreResponseDto> response = new ServiceResponse<GenreResponseDto>();
            var genreToDelete = await _context.Genres.Include(g=>g.Movies).FirstOrDefaultAsync(g => g.Id == id);
            if (genreToDelete != null)
            {
                _context.Genres.Remove(genreToDelete);
                await _context.SaveChangesAsync();
                response.Data = new GenreResponseDto
                {
                    Id = genreToDelete.Id,
                    Name = genreToDelete.Name,
                    Movies = genreToDelete.Movies == null ? genreToDelete.Movies.Select(m => m.Title).ToList() : new List<String>()
                };
                return response;
              
            }
            response.Success = false;
            response.Message = "Genre Not Found.";
            response.ResponseCode = System.Net.HttpStatusCode.NotFound;
            return response;
        }

        public async Task<ServiceResponse<List<GenreResponseDto>>> GetAllGenres()
        {
            ServiceResponse<List<GenreResponseDto>> response = new ServiceResponse<List<GenreResponseDto>>();

            var genres = await _context.Genres.Include(g => g.Movies).ToListAsync(); 
            List<GenreResponseDto> genreDtos = new List<GenreResponseDto>();
            foreach ( var genre in genres )
            {
               genreDtos.Add(_mapper.Map<GenreResponseDto>(genre));
            }
            response.Data = genreDtos;
            return response;

        }

        public async Task<ServiceResponse<GenreResponseDto>> GetGenreById(int id)
        {
            ServiceResponse<GenreResponseDto> response = new ServiceResponse<GenreResponseDto>();
            var dbGenre = await _context.Genres.Include(g=>g.Movies).FirstOrDefaultAsync(g => g.Id == id);
            GenreResponseDto genreDto = _mapper.Map<GenreResponseDto>(dbGenre);
            if(dbGenre != null)
            {
                response.Data = genreDto;
                return response;
            }
            response.Success = false;
            response.Message = $"Genre with id:{id} not found.";
            response.ResponseCode= System.Net.HttpStatusCode.NotFound;
            return response;
        }

        public async Task<ServiceResponse<GenreResponseDto>> UpdateGenre(int id, GenreDto genreDto)
        {
            ServiceResponse<GenreResponseDto> response = new ServiceResponse<GenreResponseDto>();
            Genre? genreToUpdate = await _context.Genres.Include(g=>g.Movies).FirstOrDefaultAsync(g =>g.Id == id);
            if (genreToUpdate != null)
            {
                genreToUpdate.Name = genreDto.Name;
                await _context.SaveChangesAsync();
                if(genreToUpdate.Movies != null)
                {
                    GenreResponseDto dto1 = new GenreResponseDto
                    {
                        Id = genreToUpdate.Id,
                        Name = genreToUpdate.Name,
                        Movies = genreToUpdate.Movies.Select(m => m.Title).ToList()
                    };
                    response.Data = dto1;
                    return response;
                }
                GenreResponseDto dto = new GenreResponseDto
                {
                    Id = genreToUpdate.Id,
                    Name = genreToUpdate.Name,
                    Movies = new List<String>()
                };
                response.Data = dto;
                return response;
            }

            response.Success=false;
            response.Message = $"Genre with id:{id} not found.";
            response.ResponseCode = System.Net.HttpStatusCode.NotFound;
            return response;
        }
    }
}
