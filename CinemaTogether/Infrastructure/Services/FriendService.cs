using Application.Common.Dto;
using Application.Common.Services;
using Application.Data;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class FriendService(IApplicationDbContext context) : IFriendService
{
    public async Task AddFriend(Guid userId, Guid friendId)
    {
        if (userId == friendId)
            throw new FriendAddingException("You cannot add a yourself to friends.");
        
        if (await context.UserFriends.AnyAsync(uf => uf.UserId == userId && uf.FriendId == friendId))
            throw new FriendAddingException("You are already friends.");

        var friendship = new UserFriend
        {
            UserId = userId,
            FriendId = friendId
        };

        context.UserFriends.Add(friendship);
        await context.SaveChangesAsync();
    }

    public async Task RemoveFriend(Guid userId, Guid friendId)
    {
        var friendship = await context.UserFriends
            .FirstOrDefaultAsync(uf => uf.UserId == userId && uf.FriendId == friendId);

        if (friendship == null)
            throw new NotFoundException("User Friend", "Id", friendId.ToString());

        context.UserFriends.Remove(friendship);
        await context.SaveChangesAsync();
    }

    public async Task<PaginatedResponse<UserListItemDto>> GetFriends(Guid userId, int page, int pageSize)
    {
        var friends =  await context.UserFriends
            .Where(uf => uf.UserId == userId)
            .Select(uf => new UserListItemDto(
                uf.Friend.Id,
                uf.Friend.Username,
                uf.Friend.Name,
                uf.Friend.Rating))
            .ToListAsync();
        
        var totalCount = friends.Count();

        return new PaginatedResponse<UserListItemDto>(friends, page, pageSize, totalCount);
    }
}