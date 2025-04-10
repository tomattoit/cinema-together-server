using Application.Common.Dto;
using Application.Common.Services;
using Application.Data;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace Infrastructure.Services;

public class MovieService(IApplicationDbContext context) : IMovieService
{
    public async Task<MovieDto> GetMovieById(Guid id, CancellationToken cancellationToken)
    {
        var movie = await context.Movies
            .AsNoTracking()
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
            .AsNoTracking()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(m => new MovieListItem(m.Id, m.Title, m.ReleaseDate, m.PosterPath, m.RatingTmdb))
            .ToListAsync(cancellationToken);

        var paginatedResponse = new PaginatedResponse<MovieListItem>(movies, totalCount, page, pageSize);
        
        return paginatedResponse;
    }

    public async Task RateMovie(Guid movieId, Guid userId, decimal rate, CancellationToken cancellationToken)
    {
        if (rate > 10 || rate < 0)
            throw new RateOutOfRangeException();
        
        var movie = await context.Movies.FirstOrDefaultAsync(m => m.Id == movieId, cancellationToken);
        
        if (movie == null)
            throw new NotFoundException("Movie", "Id", movieId.ToString());

        if (await context.MovieUserRates.AnyAsync(
                m => m.MovieId == movieId && m.UserId == userId,
                cancellationToken))
            throw new PropertyNotUniqueException("Rate");
        
        movie.Rating += rate;
        movie.RatingCount++;

        var rateEntity = new MovieUserRate
        {
            MovieId = movieId,
            UserId = userId
        };
        
        await context.MovieUserRates.AddAsync(rateEntity, cancellationToken);
        
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<PaginatedResponse<UserRate>> GetUserRates(
        Guid userId,
        int page,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var userRates = await context.MovieUserRates
            .AsNoTracking()
            .Include(m => m.Movie)
            .Where(m => m.UserId == userId)
            .Select(
                m => new UserRate(
                    new MovieListItem(
                        m.MovieId,
                        m.Movie.Title,
                        m.Movie.ReleaseDate,
                        m.Movie.PosterPath,
                        m.Movie.RatingTmdb),
                m.Rate))
            .ToListAsync(cancellationToken);
        
        return new PaginatedResponse<UserRate>(userRates, userRates.Count, page, pageSize);
    }
}