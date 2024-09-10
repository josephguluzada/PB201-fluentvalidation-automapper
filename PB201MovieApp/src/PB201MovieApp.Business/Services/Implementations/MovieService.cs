using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PB201MovieApp.Business.DTOs.MovieDtos;
using PB201MovieApp.Business.Exceptions.CommonExceptions;
using PB201MovieApp.Business.Services.Interfaces;
using PB201MovieApp.Core.Entities;
using PB201MovieApp.Core.Repositories;
using System.Linq.Expressions;

namespace PB201MovieApp.Business.Services.Implementations;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;
    private readonly IMapper _mapper;

    public MovieService(IMovieRepository movieRepository, IMapper mapper)
    {
        _movieRepository = movieRepository;
        _mapper = mapper;
    }

    public async Task<MovieGetDto> CreateAsync(MovieCreateDto dto)
    {
       
        Movie movie = _mapper.Map<Movie>(dto);
        movie.CreatedDate = DateTime.Now;
        movie.ModifiedDate = DateTime.Now;
        await _movieRepository.CreateAsync(movie);
        await _movieRepository.CommitAsync();
        MovieGetDto getDto = new MovieGetDto(movie.Id, movie.Title, movie.Desc, movie.IsDeleted, movie.CreatedDate, movie.ModifiedDate);

        return getDto;
    }
    public async Task DeleteAsync(int id)
    {
        if (id < 1) throw new InvalidIdException();
        var data = await _movieRepository.GetByIdAsync(id);
        if(data is null) throw new EntityNotFoundException(404,"EntityNotFound");
        _movieRepository.Delete(data);
        await _movieRepository.CommitAsync();
    }

    public async Task<ICollection<MovieGetDto>> GetByExpression(bool asNoTracking = false, Expression<Func<Movie, bool>>? expression = null, params string[] includes)
    {
        var datas = await _movieRepository.GetByExpression(asNoTracking, expression, includes).ToListAsync();

        ICollection<MovieGetDto> dtos = datas.Select(data => new MovieGetDto(data.Id, data.Title, data.Desc, data.IsDeleted, data.CreatedDate, data.ModifiedDate)).ToList();

        return dtos;
    }

    public async Task<MovieGetDto> GetById(int id)
    {
        if (id < 1) throw new InvalidIdException();
        var data = await _movieRepository.GetByIdAsync(id);

        if (data is null) throw new EntityNotFoundException(404, "EntityNotFound");

        //MovieGetDto dto = new MovieGetDto(data.Id, data.Title, data.Desc, data.IsDeleted, data.CreatedDate, data.ModifiedDate);

        MovieGetDto dto = _mapper.Map<MovieGetDto>(data);

        return dto;
    }

    public async Task<MovieGetDto> GetSingleByExpression(bool asNoTracking = false, Expression<Func<Movie, bool>>? expression = null, params string[] includes)
    {
        var data = await _movieRepository.GetByExpression(asNoTracking, expression, includes).FirstOrDefaultAsync();
        if (data is null) throw new EntityNotFoundException(404, "EntityNotFound");

        MovieGetDto dto = new MovieGetDto(data.Id, data.Title, data.Desc, data.IsDeleted, data.CreatedDate, data.ModifiedDate);

        return dto;
    }

    public async Task UpdateAsync(int? id,MovieUpdateDto dto)
    {
        if(id<1 || id is null) throw new InvalidIdException();

        var data = await _movieRepository.GetByIdAsync((int)id);

        if (data is null) throw new EntityNotFoundException();

        _mapper.Map(dto,data);

        data.ModifiedDate = DateTime.Now;

        await _movieRepository.CommitAsync();
    }
}
