using Application.Common.Dto;

namespace Application.Common.Services;

public interface IMovieService
{
    Task<MovieDto> GetMovieById(Guid id, CancellationToken cancellationToken);

    Task<PaginatedResponse<MovieListItem>> GetMovies(int page, int pageSize, CancellationToken cancellationToken);

    Task<PaginatedResponse<MovieListItem>> SearchMovies(
        string? title,
        List<Guid>? genreIds,
        DateTime? releaseDateFrom,
        DateTime? releaseDateTo,
        int page,
        int pageSize,
        CancellationToken cancellationToken);

    Task ReviewMovie(Guid movieId, Guid userId, decimal rate, string comment, CancellationToken cancellationToken);

    Task<List<MovieReviewDto>> GetMovieReviewsOfUser(Guid userId, CancellationToken cancellationToken);

    Task<PaginatedResponse<MovieReviewDto>> GetMovieReviews(Guid movieId, int page, int pageSize, CancellationToken cancellationToken);

    Task<List<GenreDto>> GetGenres(CancellationToken cancellationToken);
}