using System.Security.Claims;
using Application.Common.Dto;
using Application.Common.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route ("api/users")]
public class UserController(IUserService userService) : ControllerBase
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
    public async Task<IResult> PatchMeAsync(UpdateUserModel model, CancellationToken cancellationToken)
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
    [Produces(typeof(List<UserListItemDto>))]
    public async Task<IResult> GetUsers(CancellationToken cancellationToken)
    {
        var users = await userService.GetUsersAsync(cancellationToken);
        
        return Results.Ok(users);
    }
}