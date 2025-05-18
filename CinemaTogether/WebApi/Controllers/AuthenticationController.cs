using System.Security.Claims;
using Domain.Constants;
using Application.Common.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/authentication")]
public class AuthenticationController(
    IConfiguration configuration,
    IAuthService authService)
    : ControllerBase
{
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    [HttpPost("login")]
    public async Task<IResult> Login([FromBody] LoginModel model, CancellationToken cancellationToken)
    {
        var token = await authService.Login(model.Email, model.Password, cancellationToken);

        Response.Cookies.Append(
            CommonConstants.DefaultTokenName,
            token,
            new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("Jwt:ExpirationInMinutes"))
            });

        return Results.Ok();
    }

    [HttpPost("logout")]
    public IResult Logout()
    {
        Response.Cookies.Append(
            CommonConstants.DefaultTokenName,
            string.Empty,
            new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(-1),
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });

        return Results.Ok();
    }

    [HttpPut("password")]
    [Authorize]
    public async Task<IResult> ChangePassword(UpdatePasswordModel model, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            return Results.Unauthorized();

        await authService.UpdatePassword(userId, model.OldPassword, model.NewPassword, cancellationToken);
        
        return Results.NoContent();
    }
}