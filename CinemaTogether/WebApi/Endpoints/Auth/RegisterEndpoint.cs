using Application.Commands.Users;
using MediatR;

namespace WebApi.Endpoints.Auth;

public class RegisterEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/register", async (RegisterCommand model, ISender sender) =>
        {
            await sender.Send(new RegisterCommand(model.Email, model.Password, model.Username));
            return Results.Created();
        });
    }
}