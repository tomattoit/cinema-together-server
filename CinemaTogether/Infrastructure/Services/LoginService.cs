using Application.Common.Auth;
using Application.Common.Dto;
using Application.Common.Services;
using Application.Data;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using Shared.Cryptography;

namespace Infrastructure.Services;

public class LoginService(
    ITokenProvider tokenProvider,
    IApplicationDbContext context) : ILoginService
{
    public async Task<string> Login(string email, string password, CancellationToken cancellationToken)
    {
        var user = await context.Users
            .Include(a => a.Role)
            .Where(u => u.Email == email)
            .AsNoTracking()
            .Select(a => new UserDto(
                a.Id,
                a.Email,
                a.PasswordHash,
                a.Role.Name)
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
}