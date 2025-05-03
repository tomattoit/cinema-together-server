using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IUserService
{
    Task<User> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<User> GetUserByUsernameAsync(string username, CancellationToken cancellationToken = default);
    Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<bool> IsUserOnlineAsync(Guid userId, CancellationToken cancellationToken = default);
    Task SetUserOnlineStatusAsync(Guid userId, bool isOnline, CancellationToken cancellationToken = default);
}