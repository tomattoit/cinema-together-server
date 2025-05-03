using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Application.Common.Interfaces;
using System.Security.Claims;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ChatsController : ControllerBase
{
    private readonly IChatService _chatService;

    public ChatsController(IChatService chatService)
    {
        _chatService = chatService;
    }

    [HttpGet("{chatId}")]
    public async Task<IActionResult> GetChat(string chatId, CancellationToken cancellationToken)
    {
        var chat = await _chatService.GetChatByIdAsync(chatId, cancellationToken);
        if (chat == null)
        {
            return NotFound();
        }

        return Ok(chat);
    }

    [HttpGet("{chatId}/messages")]
    public async Task<IActionResult> GetMessages(
        string chatId,
        [FromQuery] int skip = 0,
        [FromQuery] int take = 50,
        CancellationToken cancellationToken = default)
    {
        var messages = await _chatService.GetChatMessagesAsync(chatId, skip, take, cancellationToken);
        return Ok(messages);
    }

    [HttpGet("{chatId}/pinned")]
    public async Task<IActionResult> GetPinnedMessages(string chatId, CancellationToken cancellationToken)
    {
        var messages = await _chatService.GetPinnedMessagesAsync(chatId, cancellationToken);
        return Ok(messages);
    }

    [HttpPost("{chatId}/messages")]
    public async Task<IActionResult> SendMessage(string chatId, [FromBody] SendMessageRequest request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        var message = await _chatService.SendMessageAsync(
            chatId,
            userId,
            request.Text,
            request.ImagePath,
            cancellationToken
        );

        return Ok(message);
    }

    [HttpPut("messages/{messageId}")]
    public async Task<IActionResult> EditMessage(Guid messageId, [FromBody] EditMessageRequest request, CancellationToken cancellationToken)
    {
        var message = await _chatService.EditMessageAsync(messageId, request.NewText, cancellationToken);
        if (message == null)
        {
            return NotFound();
        }

        return Ok(message);
    }

    [HttpDelete("messages/{messageId}")]
    public async Task<IActionResult> DeleteMessage(Guid messageId, CancellationToken cancellationToken)
    {
        await _chatService.DeleteMessageAsync(messageId, cancellationToken);
        return Ok();
    }

    [HttpPost("messages/{messageId}/pin")]
    public async Task<IActionResult> PinMessage(Guid messageId, CancellationToken cancellationToken)
    {
        await _chatService.PinMessageAsync(messageId, cancellationToken);
        return Ok();
    }

    [HttpPost("messages/{messageId}/unpin")]
    public async Task<IActionResult> UnpinMessage(Guid messageId, CancellationToken cancellationToken)
    {
        await _chatService.UnpinMessageAsync(messageId, cancellationToken);
        return Ok();
    }
}

public record SendMessageRequest(string Text, string? ImagePath);
public record EditMessageRequest(string NewText);