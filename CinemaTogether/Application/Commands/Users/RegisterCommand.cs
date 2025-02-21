using MediatR;

namespace Application.Commands.Users;

public record RegisterCommand(
    string Email,
    string Password,
    string Username) : IRequest;