namespace Application.Common.Services;

public interface IAuthService
{
    Task<string> Login(string email, string password, CancellationToken cancellationToken);
    
    Task UpdatePassword(Guid userId, string oldPassword, string newPassword, CancellationToken cancellationToken);
}