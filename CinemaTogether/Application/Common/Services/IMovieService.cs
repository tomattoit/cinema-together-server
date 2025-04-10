using Application.Common.Dto;
using Domain.Entities;
using WebApi.Models;

namespace Application.Common.Services;

public interface IMovieService
{
    Task<MovieDto> GetMovieById(Guid id, CancellationToken cancellationToken);
    
    Task<PaginatedResponse<MovieListItem>> GetMovies(int page, int pageSize, CancellationToken cancellationToken);
    
    Task RateMovie(Guid movieId, Guid userId, decimal rate, CancellationToken cancellationToken);
    
    Task<PaginatedResponse<UserRate>> GetUserRates(Guid userId, int page, int pageSize, CancellationToken cancellationToken);
}