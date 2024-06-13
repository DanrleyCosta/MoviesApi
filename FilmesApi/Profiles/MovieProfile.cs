using AutoMapper;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;

namespace FilmesApi.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile() 
        {
            CreateMap<CreateMovieDto, Movie>();
            CreateMap<UpdateMovieDto, Movie>();
            CreateMap<Movie, UpdateMovieDto>();
            CreateMap<Movie, ReadMovieDto>();
        }   
    }
}
