using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IGroupService
{
    Task<Group> CreateGroupAsync(string name, string description, string type, Guid ownerId, List<Guid> genreIds, CancellationToken cancellationToken = default);
    Task<Group> GetGroupByIdAsync(Guid groupId, CancellationToken cancellationToken = default);
    Task<List<Group>> GetUserGroupsAsync(Guid userId, CancellationToken cancellationToken = default);
    Task JoinGroupAsync(Guid groupId, Guid userId, CancellationToken cancellationToken = default);
    Task LeaveGroupAsync(Guid groupId, Guid userId, CancellationToken cancellationToken = default);
    Task InviteToGroupAsync(Guid groupId, Guid inviterId, Guid inviteeId, CancellationToken cancellationToken = default);
    Task<bool> IsUserInGroupAsync(Guid groupId, Guid userId, CancellationToken cancellationToken = default);
    Task<bool> IsUserGroupOwnerAsync(Guid groupId, Guid userId, CancellationToken cancellationToken = default);
    Task UpdateGroupAsync(Guid groupId, string name, string description, string type, List<Guid> genreIds, CancellationToken cancellationToken = default);
    Task DeleteGroupAsync(Guid groupId, CancellationToken cancellationToken = default);
}