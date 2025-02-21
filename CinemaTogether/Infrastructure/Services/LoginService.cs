using Application.Common.Auth;
using Application.Common.Services;
using Application.Queries.Users;
using MediatR;
using Shared.Cryptography;

namespace Infrastructure.Services;

public class LoginService(
    ITokenProvider tokenProvider,
    ISender sender) : ILoginService
{
    public async Task<string> Login(string email, string password, CancellationToken cancellationToken)
    {
        var user = await sender.Send(new GetUserByEmailQuery(email), cancellationToken);
        var isVerified = Hasher.Verify(password, user.PasswordHash);
        
        if (!isVerified)
        {
            throw new Exception();
        }

        return tokenProvider.Create(user);
    }
}