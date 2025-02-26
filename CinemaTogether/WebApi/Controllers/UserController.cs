using Application.Common.Services;
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
}