using Application.Common.Services;
using Application.Data;
using Domain.Constants;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using Shared.Cryptography;

namespace Infrastructure.Services;

public class RegisterService(IApplicationDbContext context) : IRegisterService
{
    public async Task RegisterAsync(
        string email,
        string password,
        string username,
        CancellationToken cancellationToken = default)
    {
        var isUniqueUsername = await context.Users
            .Where(u => u.Username == username)
            .ToListAsync(cancellationToken);

        if (isUniqueUsername.Count > 0)
        {
            throw new PropertyNotUniqueException("Username");
        }
        
        var isUniqueEmail = await context.Users
            .Where(u => u.Email == email)
            .ToListAsync(cancellationToken);

        if (isUniqueEmail.Count > 0)
        {
            throw new PropertyNotUniqueException("Email");
        }
        
        await context.Users.AddAsync(
            new User
            {
                Email = email,
                Username = username,
                PasswordHash = Hasher.Hash(password),
                RoleId = CommonConstants.UserRoleId
            },
            cancellationToken);
        
        await context.SaveChangesAsync(cancellationToken);
    }
}