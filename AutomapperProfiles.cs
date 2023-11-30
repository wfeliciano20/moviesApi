using AutoMapper;
using moviesApi.Dtos;
using moviesApi.Models;

namespace moviesApi
{
    public class AutomapperProfiles : Profile
    {
            public AutomapperProfiles()
            {
                CreateMap<MovieDto,Movie>();
                CreateMap<Movie, MovieDto>();
                CreateMap<GenreDto, Genre>();
                CreateMap<Genre, GenreDto>();
            }
    }
    
}
