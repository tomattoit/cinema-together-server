using System.Security.Claims;
using Application.Common.Dto;
using Application.Common.Services;
using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route ("api/users")]
public class UserController(IUserService userService, IMovieService movieService) : ControllerBase
{
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    [HttpPost("signup")]
    public async Task<IResult> SignUp([FromBody] RegisterModel model, CancellationToken cancellationToken)
    {
        await userService.RegisterAsync(model.Email, model.Password, model.Username, cancellationToken);
        
        return Results.Created();
    }

    [HttpGet("email-verification")]
    [EndpointName(CommonConstants.VerifyEmailEndpointName)]
    public async Task<IResult> VerifyEmail([FromQuery] string verificationTokenId, CancellationToken cancellationToken)
    {
        var tokenId = Guid.Parse(verificationTokenId);
        
        await userService.VerifyEmailAsync(tokenId, cancellationToken);
        
        return Results.NoContent();
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    [Produces(typeof(UserPublicInfoDto))]
    [HttpGet("{id:guid}")]
    public async Task<IResult> GetProfileInfo(Guid id, CancellationToken cancellationToken)
    {
        var user = await userService.GetUserPublicInfoByIdAsync(id, cancellationToken);
        
        return Results.Ok(user);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Produces(typeof(UserPrivateInfoDto))]
    [Authorize]
    [HttpGet("me")]
    public async Task<IResult> GetMeAsync(CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
        {
            return Results.Unauthorized();
        }

        var user = await userService.GetUserPrivateInfoByIdAsync(userId, cancellationToken);
        
        return Results.Ok(user);
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesErrorResponseType(typeof(ValidationProblemDetails))]
    [Authorize]
    [HttpPut("me")]
    public async Task<IResult> EditMyProfileAsync(UpdateUserModel model, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
        {
            return Results.Unauthorized();
        }

        var dto = new UpdateUserProfileDto(
            model.Email,
            model.Username,
            model.Name,
            model.DateOfBirth,
            model.Gender,
            model.ProfilePicturePath,
            model.CityId);
        
        await userService.UpdateProfileInfoAsync(userId, dto, cancellationToken);
        
        return Results.NoContent();
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [Produces(typeof(List<GenderDto>))]
    [HttpGet("genders")]
    public IResult GetGenders()
    {
        var genders = userService.GetGenders();
        
        return Results.Ok(genders);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Produces(typeof(PaginatedResponse<UserListItemDto>))]
    public async Task<IResult> GetUsers(
        CancellationToken cancellationToken,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string searchString = null)
    {
        var users = await userService.GetUsersAsync(
            page,
            pageSize,
            searchString,
            cancellationToken);
        
        return Results.Ok(users);
    }

    [HttpGet("{userId:guid}/reviews")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResponse<MovieReviewDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> GetUserReviews(Guid userId, CancellationToken cancellationToken, [FromQuery]int page = 1, [FromQuery]int pageSize = 10)
    {
        var reviews = await movieService.GetMovieReviewsOfUser(userId, page, pageSize, cancellationToken);
        
        return Results.Ok(reviews);
    }
    
    [HttpGet("me/reviews")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResponse<MovieReviewDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IResult> GetMyReviews(CancellationToken cancellationToken, [FromQuery]int page = 1, [FromQuery]int pageSize = 10)
    {
        if (!Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
        {
            return Results.Unauthorized();
        }
        
        var reviews = await movieService.GetMovieReviewsOfUser(userId, page, pageSize, cancellationToken);
        
        return Results.Ok(reviews);
    }
    
    [HttpPut("me/reviews/{reviewId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IResult> EditMyReview(Guid reviewId, [FromBody]UpdateReviewModel model, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
        {
            return Results.Unauthorized();
        }

        var updateReviewDto = new UpdateReviewDto(reviewId, model.Comment, model.Rate);
        
        await movieService.UpdateMovieReviewOfUser(updateReviewDto, userId, cancellationToken);
        
        return Results.NoContent();
    }
    
    [HttpDelete("me/reviews/{reviewId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IResult> DeleteMyReview(Guid reviewId, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
        {
            return Results.Unauthorized();
        }
        
        await movieService.DeleteMovieReviewOfUser(userId, reviewId, cancellationToken);
        
        return Results.NoContent();
    }

    [HttpPut("me/favourite/genres")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    public async Task<IResult> SetMyFavouriteGenre([FromBody] List<Guid> genresIds, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
        {
            return Results.Unauthorized();
        }
        
        await userService.SetUserFavouriteGenresAsync(userId, genresIds, cancellationToken);
        
        return Results.NoContent();
    }

    [HttpGet("me/favourite/genres")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    public async Task<IResult> GetMyFavouriteGenres(CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
        {
            return Results.Unauthorized();
        }
        
        var genres = await userService.GetUserFavouriteGenresAsync(userId, cancellationToken);
        
        return Results.Ok(genres);
    }
}