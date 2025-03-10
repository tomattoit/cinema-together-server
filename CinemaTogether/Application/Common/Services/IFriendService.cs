using Application.Common.Dto;
using Domain.Entities;

namespace Application.Common.Services;

public interface IFriendService
{
    Task AddFriend(Guid userId, Guid friendId);

    Task RemoveFriend(Guid userId, Guid friendId);

    Task<List<UserListItemDto>> GetFriends(Guid userId);
}