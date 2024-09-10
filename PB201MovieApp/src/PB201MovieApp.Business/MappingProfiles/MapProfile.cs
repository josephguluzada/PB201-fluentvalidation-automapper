using AutoMapper;
using PB201MovieApp.Business.DTOs.MovieDtos;
using PB201MovieApp.Core.Entities;

namespace PB201MovieApp.Business.MappingProfiles;

public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<Movie, MovieGetDto>().ReverseMap();
        CreateMap<MovieCreateDto, Movie>().ReverseMap();
        CreateMap<MovieUpdateDto, Movie>().ReverseMap();
    }
}
