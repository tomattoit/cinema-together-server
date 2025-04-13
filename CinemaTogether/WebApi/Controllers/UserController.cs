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
    public async Task<IResult> GetUserReviews(Guid userId, int page, int pageSize, CancellationToken cancellationToken)
    {
        var reviews = await movieService.GetUserReviews(userId, page, pageSize, cancellationToken);
        
        return Results.Ok(reviews);
    }
    
    [HttpGet("me/reviews")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResponse<MovieReviewDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IResult> GetMyReviews(int page, int pageSize, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
        {
            return Results.Unauthorized();
        }
        
        var reviews = await movieService.GetUserReviews(userId, page, pageSize, cancellationToken);
        
        return Results.Ok(reviews);
    }
}