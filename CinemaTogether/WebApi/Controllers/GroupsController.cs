using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Application.Common.Interfaces;
using System.Security.Claims;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class GroupsController : ControllerBase
{
    private readonly IGroupService _groupService;

    public GroupsController(IGroupService groupService)
    {
        _groupService = groupService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateGroup([FromBody] CreateGroupRequest request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        var group = await _groupService.CreateGroupAsync(
            request.Name,
            request.Description,
            request.Type,
            userId,
            request.GenreIds,
            cancellationToken
        );

        return Ok(group);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGroup(Guid id, CancellationToken cancellationToken)
    {
        var group = await _groupService.GetGroupByIdAsync(id, cancellationToken);
        if (group == null)
        {
            return NotFound();
        }

        return Ok(group);
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetMyGroups(CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        var groups = await _groupService.GetUserGroupsAsync(userId, cancellationToken);
        return Ok(groups);
    }

    [HttpPost("{id}/join")]
    public async Task<IActionResult> JoinGroup(Guid id, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        await _groupService.JoinGroupAsync(id, userId, cancellationToken);
        return Ok();
    }

    [HttpPost("{id}/leave")]
    public async Task<IActionResult> LeaveGroup(Guid id, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        await _groupService.LeaveGroupAsync(id, userId, cancellationToken);
        return Ok();
    }

    [HttpPost("{id}/invite")]
    public async Task<IActionResult> InviteToGroup(Guid id, [FromBody] InviteToGroupRequest request, CancellationToken cancellationToken)
    {
        var inviterId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        await _groupService.InviteToGroupAsync(id, inviterId, request.InviteeId, cancellationToken);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGroup(Guid id, [FromBody] UpdateGroupRequest request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        if (!await _groupService.IsUserGroupOwnerAsync(id, userId, cancellationToken))
        {
            return Forbid();
        }

        await _groupService.UpdateGroupAsync(
            id,
            request.Name,
            request.Description,
            request.Type,
            request.GenreIds,
            cancellationToken
        );

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGroup(Guid id, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        if (!await _groupService.IsUserGroupOwnerAsync(id, userId, cancellationToken))
        {
            return Forbid();
        }

        await _groupService.DeleteGroupAsync(id, cancellationToken);
        return Ok();
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