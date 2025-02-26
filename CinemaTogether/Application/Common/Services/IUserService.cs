using Application.Common.Dto;
using Domain.Entities;
using Microsoft.AspNetCore.JsonPatch;

namespace Application.Common.Services;

public interface IUserService
{
    Task<UserPublicInfoDto> GetUserPublicInfoByIdAsync(Guid userId, CancellationToken cancellationToken);
    
    Task<UserPrivateInfoDto> GetUserPrivateInfoByIdAsync(Guid userId, CancellationToken cancellationToken);
    
    Task RegisterAsync(string email, string password, string username, CancellationToken cancellationToken);
    
    Task UpdateProfileInfo(Guid userId, UpdateUserInfoDto userDto, CancellationToken cancellationToken);
}