using Application.Common.Dto;

namespace Application.Common.Services;

public interface IFriendService
{
    Task AddFriend(Guid userId, Guid friendId);

    Task RemoveFriend(Guid userId, Guid friendId);

    Task<PaginatedResponse<UserListItemDto>> GetFriends(Guid userId, int page, int pageSize);
}