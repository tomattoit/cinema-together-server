using Application.Common.Dto;
using Application.Common.Services;
using Application.Data;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace Infrastructure.Services;

public class MovieService(IApplicationDbContext context) : IMovieService
{
    public async Task<MovieDto> GetMovieById(Guid id, CancellationToken cancellationToken)
    {
        var movie = await context.Movies
            .Where(m => m.Id == id)
            .Include(m => m.MovieGenres)
            .Select(m => new MovieDto(
                m.Id,
                m.Title,
                m.Duration,
                m.MovieGenres.Select(g => g.Genre.Name).ToList(),
                m.Description,
                m.Director,
                m.Actors,
                m.ReleaseDate,
                m.PosterPath,
                m.RatingTmdb,
                m.Rating,
                m.RatingCount
                ))
            .FirstOrDefaultAsync(cancellationToken);
        
        if (movie == null)
            throw new NotFoundException("Movie", "Id", id.ToString());
        
        return movie;
    }

    public async Task<PaginatedResponse<MovieListItem>> GetMovies(int page, int pageSize, CancellationToken cancellationToken)
    {
        var query = context.Movies.AsQueryable();
            
        var totalCount = await query.CountAsync(cancellationToken);
        
        var movies = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(m => new MovieListItem(m.Id, m.Title, m.ReleaseDate, m.PosterPath, m.RatingTmdb))
            .ToListAsync(cancellationToken);

        var paginatedResponse = new PaginatedResponse<MovieListItem>(movies, totalCount, page, pageSize);
        
        return paginatedResponse;
    }

    public async Task RateMovie(Guid movieId, Guid userId, decimal rate, CancellationToken cancellationToken)
    {
        var movie = await context.Movies.FirstOrDefaultAsync(m => m.Id == movieId, cancellationToken);
        
        if (movie == null)
            throw new NotFoundException("Movie", "Id", movieId.ToString());
        
        movie.Rating += rate;
        movie.RatingCount++;
        
        await context.SaveChangesAsync(cancellationToken);
    }
}