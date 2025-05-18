using System.Security.Claims;
using Application.Common.Dto;
using Application.Common.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace WebApi.Controllers;

[ApiController]
[Route ("api/friends")]
public class FriendController(IFriendService friendService) : ControllerBase
{
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize]
    [HttpPost("add/{friendId:guid}")]
    public async Task<IResult> AddFriend(Guid friendId)
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            return Results.Unauthorized();
        
        await friendService.AddFriend(userId, friendId);

        return Results.Ok();
    }

    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize]
    [HttpDelete("remove/{friendId:guid}")]
    public async Task<IResult> RemoveFriend(Guid friendId)
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            return Results.Unauthorized();
        
        await friendService.RemoveFriend(userId, friendId);

        return Results.Ok();
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Produces(typeof(List<UserListItemDto>))]
    [HttpGet("me/requests/sent")]
    [Authorize]
    public async Task<IResult> GetMySentRequests([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            return Results.Unauthorized();
        
        var friends = await friendService.GetSentFriendRequests(userId, page, pageSize);
        
        return Results.Ok(friends);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Produces(typeof(List<UserListItemDto>))]
    [HttpGet("me/requests/received")]
    [Authorize]
    public async Task<IResult> GetMyReceivedRequests([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            return Results.Unauthorized();
        
        var friends = await friendService.GetReceivedFriendRequests(userId, page, pageSize);
        
        return Results.Ok(friends);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Produces(typeof(List<UserListItemDto>))]
    [HttpGet("me")]
    [Authorize]
    public async Task<IResult> GetMyFriends([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            return Results.Unauthorized();
        
        var friends = await friendService.GetFriends(userId, page, pageSize);
        
        return Results.Ok(friends);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Produces(typeof(List<UserListItemDto>))]
    [HttpGet("{userId:guid}")]
    [Authorize]
    public async Task<IResult> GetUserFriends(Guid userId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var friends = await friendService.GetFriends(userId, page, pageSize);
        
        return Results.Ok(friends);
    }
}