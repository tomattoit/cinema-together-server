using Application.Common.Dto;
using WebApi.Models;

namespace Application.Common.Services;

public interface IMovieService
{
    Task<MovieDto> GetMovieById(Guid id, CancellationToken cancellationToken);
    
    Task<PaginatedResponse<MovieListItem>> GetMovies(int page, int pageSize, CancellationToken cancellationToken);
    
    Task RateMovie(Guid movieId, Guid userId, decimal rate, CancellationToken cancellationToken);
}