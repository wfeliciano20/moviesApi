using AutoMapper;
using moviesApi.Dtos;
using moviesApi.Models;

namespace moviesApi
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<MovieDto, Movie>();
            CreateMap<Movie, MovieDto>();
            var mapToMovieResponseDto = CreateMap<Movie, MovieResponseDto>();
            mapToMovieResponseDto.AfterMap((src, dest) =>
            {
                dest.Genres = src.Genres.Select(g => g.Name).ToList();
            });
            CreateMap<GenreDto, Genre>();
            CreateMap<Genre, GenreDto>();
            var mapToGenreResponseDto = CreateMap<Genre, GenreResponseDto>();
            mapToGenreResponseDto.AfterMap((src, dest) =>
            {
                dest.Movies = src.Movies.Select(m => m.Title).ToList();
            });
        }
    }
    
}
