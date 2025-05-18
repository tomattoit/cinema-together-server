using Application.Common.Auth;
using Application.Common.Dto;
using Application.Common.Services;
using Application.Data;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using Shared.Cryptography;

namespace Infrastructure.Services;

public class AuthService(
    ITokenProvider tokenProvider,
    IApplicationDbContext context) : IAuthService
{
    public async Task<string> Login(string email, string password, CancellationToken cancellationToken)
    {
        var user = await context.Users
            .Where(u => u.Email == email)
            .AsNoTracking()
            .Select(a => new UserAccountInfoDto(
                a.Id,
                a.Email,
                a.PasswordHash,
                a.Role)
            )
            .FirstOrDefaultAsync(cancellationToken);

        if (user == null)
            throw new NotFoundException("User", "email", email);
        
        var isVerified = Hasher.Verify(password, user.PasswordHash);
        
        if (!isVerified)
        {
            throw new Exception();
        }

        return tokenProvider.Create(user);
    }

    public async Task UpdatePassword(Guid userId, string oldPassword, string newPassword, CancellationToken cancellationToken)
    {
        var user = await context.Users.FindAsync(userId, cancellationToken);
        
        if (user == null) throw new NotFoundException("User", "Id", userId.ToString());
        
        var isVerified = Hasher.Verify(oldPassword, user.PasswordHash);
        
        if (!isVerified)
        {
            throw new Exception();
        }
        
        user.PasswordHash = Hasher.Hash(newPassword);
        
        await context.SaveChangesAsync(cancellationToken);
    }
}