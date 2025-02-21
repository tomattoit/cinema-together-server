using Application.Common.Services;
using Domain.Constants;
using WebApi.Models;

namespace WebApi.Endpoints.Auth;

public class LoginEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/login", async (LoginModel model,
            ILoginService loginService,
            IConfiguration configuration,
            HttpContext context,
            CancellationToken cancellationToken) =>
        {
            var token = await loginService.Login(model.Email, model.Password, cancellationToken);
            
            context.Response.Cookies.Append(
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
        });
    }
}

