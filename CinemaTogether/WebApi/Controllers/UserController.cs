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
    [HttpPost("signup")]
    public async Task<IResult> SignUp([FromBody] RegisterModel model, CancellationToken cancellationToken)
    {
        await userService.RegisterAsync(model.Email, model.Password, model.Username, cancellationToken);

        return Results.Created();
    }

    [HttpGet("{id:guid}")]
    public async Task<IResult> GetProfileInfo(Guid id, CancellationToken cancellationToken)
    {
        var user = await userService.GetUserPublicInfoByIdAsync(id, cancellationToken);
        
        return Results.Ok(user);
    }

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

    [Authorize]
    [HttpPut("me")]
    public async Task<IResult> PatchMeAsync(UpdateUserModel model, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
        {
            return Results.Unauthorized();
        }

        var dto = new UpdateUserInfoDto(
            model.Email,
            model.Username,
            model.Name,
            model.DateOfBirth,
            model.Gender,
            model.ProfilePicturePath,
            model.CityId);
        
        await userService.UpdateProfileInfo(userId, dto, cancellationToken);
        
        return Results.NoContent();
    }

    [HttpGet("genders")]
    public IResult GetGenders()
    {
        var genders = userService.GetGenders();
        
        return Results.Ok(genders);
    }
}