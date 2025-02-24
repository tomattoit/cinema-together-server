using Application.Common.Auth;
using Domain.Constants;
using Application.Commands.Users;
using Application.Common.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/authentication")]
public class AuthenticationController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IConfiguration _configuration;
    private readonly ILoginService _loginService;

    public AuthenticationController(
        ISender sender,
        IConfiguration configuration,
        ITokenProvider tokenProvider,
        ILoginService loginService)
    {
        _sender = sender;
        _configuration = configuration;
        _loginService = loginService;
    }

    [HttpPost("login")]
    public async Task<IResult> Login([FromBody] LoginModel model, CancellationToken cancellationToken)
    {
        var token = await _loginService.Login(model.Email, model.Password, cancellationToken);

        Response.Cookies.Append(
            CommonConstants.DefaultTokenName,
            token,
            new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("Jwt:ExpirationInMinutes"))
            });

        return Results.Ok();
    }

    [HttpPost("signup")]
    public async Task<IResult> SignUp([FromBody] RegisterModel model, CancellationToken cancellationToken)
    {
        await _sender.Send(
            new RegisterCommand(
                model.Email,
                model.Password,
                model.Username),
            cancellationToken);

        return Results.Created();
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