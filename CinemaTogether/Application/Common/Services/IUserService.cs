using Application.Common.Dto;

namespace Application.Common.Services;

public interface IUserService
{
    Task<UserPublicInfoDto> GetUserPublicInfoByIdAsync(Guid userId, CancellationToken cancellationToken);
    
    Task RegisterAsync(string email, string password, string username, CancellationToken cancellationToken);
}