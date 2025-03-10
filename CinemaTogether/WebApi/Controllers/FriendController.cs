using System.Security.Claims;
using Application.Common.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace WebApi.Controllers;

[ApiController]
[Route ("api/friends")]
public class FriendController(IFriendService friendService) : ControllerBase
{
    [Authorize]
    [HttpPost("add/{friendId:guid}")]
    public async Task<IResult> AddFriend(Guid friendId)
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            return Results.Unauthorized();
        
        await friendService.AddFriend(userId, friendId);

        return Results.Ok();
    }

    [Authorize]
    [HttpDelete("remove")]
    public async Task<IResult> RemoveFriend(Guid friendId)
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            return Results.Unauthorized();
        
        await friendService.RemoveFriend(userId, friendId);

        return Results.Ok();
    }

    [HttpGet("{userId}")]
    public async Task<IResult> GetFriends(Guid userId)
    {
        var friends = await friendService.GetFriends(userId);
        
        return Results.Ok(friends);
    }
    
    [HttpGet("me")]
    public async Task<IResult> GetMyFriends()
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            return Results.Unauthorized();
        
        var friends = await friendService.GetFriends(userId);
        
        return Results.Ok(friends);
    }
}