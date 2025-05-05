using Application.Common.Dto;
using Domain.Entities;

namespace Application.Common.Services;

public interface IUserService
{
    Task<UserPublicInfoDto> GetUserPublicInfoByIdAsync(Guid userId, CancellationToken cancellationToken);
    
    Task<UserPrivateInfoDto> GetUserPrivateInfoByIdAsync(Guid userId, CancellationToken cancellationToken);
    
    Task RegisterAsync(string email, string password, string username, CancellationToken cancellationToken);
    
    Task VerifyEmailAsync(Guid tokenId, CancellationToken cancellationToken);
    
    Task UpdateProfileInfoAsync(Guid userId, UpdateUserProfileDto userDto, CancellationToken cancellationToken);
    
    List<GenderDto> GetGenders();
    
    Task<PaginatedResponse<UserListItemDto>> GetUsersAsync(int page, int pageSize, string searchString, CancellationToken cancellationToken);
    
    Task<User> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default);
    /* Task<User> GetUserByUsernameAsync(string username, CancellationToken cancellationToken = default);
    Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<bool> IsUserOnlineAsync(Guid userId, CancellationToken cancellationToken = default);
    Task SetUserOnlineStatusAsync(Guid userId, bool isOnline, CancellationToken cancellationToken = default); */
}