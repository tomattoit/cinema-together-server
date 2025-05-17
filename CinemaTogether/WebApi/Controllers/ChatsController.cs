using System.Security.Claims;
using Application.Common.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ChatsController(IChatService chatService) : ControllerBase
{
    [HttpGet("{chatId}")]
    public async Task<IResult> GetChat(string chatId, CancellationToken cancellationToken)
    {
        var chat = await chatService.GetChatByIdAsync(chatId, cancellationToken);
        if (chat == null)
        {
            return Results.NotFound();
        }

        return Results.Ok(chat);
    }

    [HttpGet("{chatId}/messages")]
    public async Task<IResult> GetMessages(
        string chatId,
        [FromQuery] int skip = 0,
        [FromQuery] int take = 50,
        CancellationToken cancellationToken = default)
    {
        var messages = await chatService.GetChatMessagesAsync(chatId, skip, take, cancellationToken);
        return Results.Ok(messages);
    }

    [HttpGet("{chatId}/pinned")]
    public async Task<IResult> GetPinnedMessages(string chatId, CancellationToken cancellationToken)
    {
        var messages = await chatService.GetPinnedMessagesAsync(chatId, cancellationToken);
        return Results.Ok(messages);
    }

    [HttpPost("{chatId}/messages")]
    public async Task<IResult> SendMessage(string chatId, [FromBody] SendMessageRequest request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        var message = await chatService.SendMessageAsync(
            chatId,
            userId,
            request.Text,
            request.ImagePath,
            cancellationToken
        );

        return Results.Ok(message);
    }

    [HttpPut("messages/{messageId}")]
    public async Task<IResult> EditMessage(Guid messageId, [FromBody] EditMessageRequest request, CancellationToken cancellationToken)
    {
        var message = await chatService.EditMessageAsync(messageId, request.NewText, cancellationToken);
        if (message == null)
        {
            return Results.NotFound();
        }

        return Results.Ok(message);
    }

    [HttpDelete("messages/{messageId}")]
    public async Task<IResult> DeleteMessage(Guid messageId, CancellationToken cancellationToken)
    {
        await chatService.DeleteMessageAsync(messageId, cancellationToken);
        return Results.Ok();
    }

    [HttpPost("messages/{messageId}/pin")]
    public async Task<IResult> PinMessage(Guid messageId, CancellationToken cancellationToken)
    {
        await chatService.PinMessageAsync(messageId, cancellationToken);
        return Results.Ok();
    }

    [HttpPost("messages/{messageId}/unpin")]
    public async Task<IResult> UnpinMessage(Guid messageId, CancellationToken cancellationToken)
    {
        await chatService.UnpinMessageAsync(messageId, cancellationToken);
        return Results.Ok();
    }
}

public record SendMessageRequest(string Text, string? ImagePath);
public record EditMessageRequest(string NewText);