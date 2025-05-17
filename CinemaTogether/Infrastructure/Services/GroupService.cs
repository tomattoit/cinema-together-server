using Application.Common.Services;
using Application.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class GroupService : IGroupService
{
    private readonly IApplicationDbContext _context;
    private readonly IChatService _chatService;

    public GroupService(IApplicationDbContext context, IChatService chatService)
    {
        _context = context;
        _chatService = chatService;
    }

    public async Task<Group> CreateGroupAsync(string name, string description, string type, Guid ownerId, List<Guid> genreIds, CancellationToken cancellationToken = default)
    {
        // Создаем чат для группы
        var chat = await _chatService.CreateChatAsync(type, cancellationToken);

        var group = new Group
        {
            Name = name,
            Description = description,
            Type = type,
            OwnerId = ownerId,
            ChatId = chat.Id
        };

        _context.Groups.Add(group);
        await _context.SaveChangesAsync(cancellationToken);

        // Добавляем владельца как участника группы
        var owner = await _context.Users.FindAsync(new object[] { ownerId }, cancellationToken);
        if (owner != null)
        {
            group.Members.Add(owner);
        }

        // Добавляем выбранные жанры
        if (genreIds != null && genreIds.Any())
        {
            var genres = await _context.Genres
                .Where(g => genreIds.Contains(g.Id))
                .ToListAsync(cancellationToken);
            foreach (var genre in genres)
            {
                group.PreferredGenres.Add(genre);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);
        return group;
    }

    public async Task<Group> GetGroupByIdAsync(Guid groupId, CancellationToken cancellationToken = default)
    {
        return await _context.Groups
            .Include(g => g.Members)
            .Include(g => g.PreferredGenres)
            .Include(g => g.Owner)
            .Include(g => g.Chat)
            .FirstOrDefaultAsync(g => g.Id == groupId, cancellationToken);
    }

    public async Task<List<Group>> GetUserGroupsAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.Groups
            .Include(g => g.Members)
            .Include(g => g.PreferredGenres)
            .Where(g => g.Members.Any(m => m.Id == userId))
            .ToListAsync(cancellationToken);
    }

    public async Task JoinGroupAsync(Guid groupId, Guid userId, CancellationToken cancellationToken = default)
    {
        var group = await GetGroupByIdAsync(groupId, cancellationToken);
        var user = await _context.Users.FindAsync(new object[] { userId }, cancellationToken);

        if (group != null && user != null && !await IsUserInGroupAsync(groupId, userId, cancellationToken))
        {
            group.Members.Add(user);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task LeaveGroupAsync(Guid groupId, Guid userId, CancellationToken cancellationToken = default)
    {
        var group = await GetGroupByIdAsync(groupId, cancellationToken);
        var user = await _context.Users.FindAsync(new object[] { userId }, cancellationToken);

        if (group != null && user != null && await IsUserInGroupAsync(groupId, userId, cancellationToken))
        {
            if (group.OwnerId != userId) // Владелец не может покинуть группу
            {
                group.Members.Remove(user);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }

    public async Task InviteToGroupAsync(Guid groupId, Guid inviterId, Guid inviteeId, CancellationToken cancellationToken = default)
    {
        if (await IsUserGroupOwnerAsync(groupId, inviterId, cancellationToken))
        {
            await JoinGroupAsync(groupId, inviteeId, cancellationToken);
        }
    }

    public async Task<bool> IsUserInGroupAsync(Guid groupId, Guid userId, CancellationToken cancellationToken = default)
    {
        var group = await GetGroupByIdAsync(groupId, cancellationToken);
        return group?.Members.Any(m => m.Id == userId) ?? false;
    }

    public async Task<bool> IsUserGroupOwnerAsync(Guid groupId, Guid userId, CancellationToken cancellationToken = default)
    {
        var group = await GetGroupByIdAsync(groupId, cancellationToken);
        return group?.OwnerId == userId;
    }

    public async Task UpdateGroupAsync(Guid groupId, string name, string description, string type, List<Guid> genreIds, CancellationToken cancellationToken = default)
    {
        var group = await GetGroupByIdAsync(groupId, cancellationToken);
        if (group != null)
        {
            group.Name = name;
            group.Description = description;
            group.Type = type;

            // Обновляем жанры
            group.PreferredGenres.Clear();
            if (genreIds != null && genreIds.Any())
            {
                var genres = await _context.Genres
                    .Where(g => genreIds.Contains(g.Id))
                    .ToListAsync(cancellationToken);
                foreach (var genre in genres)
                {
                    group.PreferredGenres.Add(genre);
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task DeleteGroupAsync(Guid groupId, CancellationToken cancellationToken = default)
    {
        var group = await GetGroupByIdAsync(groupId, cancellationToken);
        if (group != null)
        {
            _context.Groups.Remove(group);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}