using Domain.Constants;
using Application.Common.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/authentication")]
public class AuthenticationController(
    IConfiguration configuration,
    ILoginService loginService)
    : ControllerBase
{
    [HttpPost("login")]
    public async Task<IResult> Login([FromBody] LoginModel model, CancellationToken cancellationToken)
    {
        var token = await loginService.Login(model.Email, model.Password, cancellationToken);

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
}