using Application.Common.Services;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route ("api/users")]
public class UserController(IRegisterService registerService) : ControllerBase
{
    [HttpPost("signup")]
    public async Task<IResult> SignUp([FromBody] RegisterModel model, CancellationToken cancellationToken)
    {
        await registerService.RegisterAsync(model.Email, model.Password, model.Username, cancellationToken);

        return Results.Created();
    }
}