using Application.Data;
using Domain.Constants;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Cryptography;

namespace Application.Commands.Users.Handlers;

public class RegisterCommandHandler(IApplicationDbContext context)
    : IRequestHandler<RegisterCommand>
{
    public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var isUnique = await context.Users
            .Where(u => u.Username == request.Username || u.Email == request.Email)
            .ToListAsync(cancellationToken);

        if (isUnique.Count > 0)
        {
            throw new Exception($"User with username {request.Username} already exists.");
        }
        
        await context.Users.AddAsync(
            new User
            {
                Email = request.Email,
                Username = request.Username,
                PasswordHash = Hasher.Hash(request.Password),
                RoleId = CommonConstants.UserRoleId
            },
            cancellationToken);
        
        await context.SaveChangesAsync(cancellationToken);
    }
}