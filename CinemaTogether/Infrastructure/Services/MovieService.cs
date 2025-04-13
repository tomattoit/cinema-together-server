using Application.Common.Dto;
using Application.Common.Services;
using Application.Data;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

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
        
        if (totalCount < 1)
            throw new NotFoundException("Movie", "Id", "Any");
        
        var movies = await query
            .AsNoTracking()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(m => new MovieListItem(m.Id, m.Title, m.ReleaseDate, m.PosterPath, m.RatingTmdb))
            .ToListAsync(cancellationToken);

        var paginatedResponse = new PaginatedResponse<MovieListItem>(movies, totalCount, page, pageSize);
        
        return paginatedResponse;
    }

    public async Task ReviewMovie(Guid movieId, Guid userId, decimal rate, string comment, CancellationToken cancellationToken)
    {
        if (rate > 10 || rate < 0)
            throw new RateOutOfRangeException();
        
        var movie = await context.Movies.FirstOrDefaultAsync(m => m.Id == movieId, cancellationToken);
        
        if (movie == null)
            throw new NotFoundException("Movie", "Id", movieId.ToString());

        if (await context.MovieReviews.AnyAsync(
                m => m.MovieId == movieId && m.UserId == userId,
                cancellationToken))
            throw new PropertyNotUniqueException("Rate");
        
        movie.Rating += rate;
        movie.RatingCount++;

        var rateEntity = new Domain.Entities.MovieReview
        {
            MovieId = movieId,
            UserId = userId
        };
        
        await context.MovieReviews.AddAsync(rateEntity, cancellationToken);
        
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<PaginatedResponse<MovieReviewDto>> GetMovieReviewsOfUser(
        Guid userId,
        int page,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var reviews = await context.MovieReviews
            .AsNoTracking()
            .Include(m => m.Movie)
            .Where(m => m.UserId == userId)
            .Select(
                m => new MovieReviewDto(
                    new MovieListItem(
                        m.MovieId,
                        m.Movie.Title,
                        m.Movie.ReleaseDate,
                        m.Movie.PosterPath,
                        m.Movie.RatingTmdb),
                    m.Rate,
                    m.Comment))
            .ToListAsync(cancellationToken);
        
        if (reviews == null)
            throw new NotFoundException("Reviews", "User Id", userId.ToString());
        
        return new PaginatedResponse<MovieReviewDto>(reviews, reviews.Count, page, pageSize);
    }

    public async Task<PaginatedResponse<MovieReviewDto>> GetMovieReviews(Guid movieId, int page, int pageSize, CancellationToken cancellationToken)
    {
        var reviews = await context.MovieReviews
            .AsNoTracking()
            .Include(m => m.Movie)
            .Where(m => m.MovieId == movieId)
            .Select(
                m => new MovieReviewDto(
                    new MovieListItem(
                        m.MovieId,
                        m.Movie.Title,
                        m.Movie.ReleaseDate,
                        m.Movie.PosterPath,
                        m.Movie.RatingTmdb),
                    m.Rate,
                    m.Comment))
            .ToListAsync(cancellationToken);
        
        if (reviews == null)
            throw new NotFoundException("Reviews", "Movie Id", movieId.ToString());
        
        return new PaginatedResponse<MovieReviewDto>(reviews, reviews.Count, page, pageSize);
    }
}