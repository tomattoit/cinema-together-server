using System.Security.Claims;
using Application.Common.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class GroupsController(IGroupService groupService) : ControllerBase
{
    [HttpPost]
    public async Task<IResult> CreateGroup([FromBody] CreateGroupRequest request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        var group = await groupService.CreateGroupAsync(
            request.Name,
            request.Description,
            request.Type,
            userId,
            request.GenreIds,
            cancellationToken
        );

        return Results.Ok(group);
    }

    [HttpGet("{id}")]
    public async Task<IResult> GetGroup(Guid id, CancellationToken cancellationToken)
    {
        var group = await groupService.GetGroupByIdAsync(id, cancellationToken);
        if (group == null)
        {
            return Results.NotFound();
        }

        return Results.Ok(group);
    }

    [HttpGet("my")]
    public async Task<IResult> GetMyGroups(CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        var groups = await groupService.GetUserGroupsAsync(userId, cancellationToken);
        return Results.Ok(groups);
    }

    [HttpPost("{id}/join")]
    public async Task<IResult> JoinGroup(Guid id, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        await groupService.JoinGroupAsync(id, userId, cancellationToken);
        return Results.Ok();
    }

    [HttpPost("{id}/leave")]
    public async Task<IResult> LeaveGroup(Guid id, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        await groupService.LeaveGroupAsync(id, userId, cancellationToken);
        return Results.Ok();
    }

    [HttpPost("{id}/invite")]
    public async Task<IResult> InviteToGroup(Guid id, [FromBody] InviteToGroupRequest request, CancellationToken cancellationToken)
    {
        var inviterId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        await groupService.InviteToGroupAsync(id, inviterId, request.InviteeId, cancellationToken);
        return Results.Ok();
    }

    [HttpPut("{id}")]
    public async Task<IResult> UpdateGroup(Guid id, [FromBody] UpdateGroupRequest request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        if (!await groupService.IsUserGroupOwnerAsync(id, userId, cancellationToken))
        {
            return Results.Forbid();
        }

        await groupService.UpdateGroupAsync(
            id,
            request.Name,
            request.Description,
            request.Type,
            request.GenreIds,
            cancellationToken
        );

        return Results.Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IResult> DeleteGroup(Guid id, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        if (!await groupService.IsUserGroupOwnerAsync(id, userId, cancellationToken))
        {
            return Results.Forbid();
        }

        await groupService.DeleteGroupAsync(id, cancellationToken);
        return Results.Ok();
    }
}

public record CreateGroupRequest(
    string Name,
    string Description,
    string Type,
    List<Guid> GenreIds
);

public record UpdateGroupRequest(
    string Name,
    string Description,
    string Type,
    List<Guid> GenreIds
);

public record InviteToGroupRequest(Guid InviteeId);