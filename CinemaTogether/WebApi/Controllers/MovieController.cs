using System.Security.Claims;
using Application.Common.Dto;
using Application.Common.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/movies")]
public class MovieController(IMovieService movieService) : ControllerBase
{
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> GetMovieAsync(Guid id, CancellationToken cancellationToken)
    {
        var movie = await movieService.GetMovieById(id, cancellationToken);

        return Results.Ok(movie);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResponse<MovieListItem>))]
    public async Task<IResult> GetMoviesAsync(
        CancellationToken cancellationToken,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var movies = await movieService.GetMovies(
            page,
            pageSize,
            cancellationToken);

        return Results.Ok(movies);
    }

    [HttpGet("search")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResponse<MovieListItem>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> SearchMoviesAsync(
        [FromQuery] MovieSearchModel searchModel,
        CancellationToken cancellationToken)
    {
        var movies = await movieService.SearchMovies(
            searchModel.Title,
            searchModel.GenreIds,
            searchModel.ReleaseDateFrom,
            searchModel.ReleaseDateTo,
            searchModel.Page,
            searchModel.PageSize,
            cancellationToken);

        return Results.Ok(movies);
    }

    [HttpPost("{id:guid}/reviews")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IResult> RateMovie(ReviewModel model, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            return Results.Unauthorized();

        await movieService.ReviewMovie(model.MovieId, userId, model.Rating, model.Comment, cancellationToken);

        return Results.Created();
    }

    [HttpGet("{id:guid}/reviews")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResponse<MovieReviewDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> GetMovieReviews(Guid movieId, CancellationToken cancellationToken, [FromQuery]int page = 1, [FromQuery]int pageSize = 10)
    {
        var reviews = await movieService.GetMovieReviews(movieId, page, pageSize, cancellationToken);

        return Results.Ok(reviews);
    }

    [HttpGet("genres")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GenreDto>))]
    public async Task<IResult> GetGenresAsync(CancellationToken cancellationToken)
    {
        var genres = await movieService.GetGenres(cancellationToken);

        return Results.Ok(genres);
    }
}