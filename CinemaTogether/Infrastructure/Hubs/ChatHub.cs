using Application.Common.Dto;
using Microsoft.AspNetCore.SignalR;
using Application.Common.Interfaces;
using Application.Common.Services;

namespace Infrastructure.Hubs;

public class ChatHub : Hub
{
    private readonly IChatService _chatService;
    private readonly IUserService _userService;

    public ChatHub(IChatService chatService, IUserService userService)
    {
        _chatService = chatService;
        _userService = userService;
    }

    public override async Task OnConnectedAsync()
    {
        var userId = Context.User?.FindFirst("sub")?.Value;
        if (!string.IsNullOrEmpty(userId))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userId);
        }
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = Context.User?.FindFirst("sub")?.Value;
        if (!string.IsNullOrEmpty(userId))
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);
        }
        await base.OnDisconnectedAsync(exception);
    }

    public async Task JoinChat(string chatId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
    }

    public async Task LeaveChat(string chatId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId);
    }

    public async Task SendMessage(string chatId, string text, string? imagePath = null)
    {
        var userId = Context.User?.FindFirst("sub")?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            throw new UnauthorizedAccessException("User is not authenticated");
        }

        var userGuid = Guid.Parse(userId);
        var message = await _chatService.SendMessageAsync(chatId, userGuid, text, imagePath);

        var sender = await _userService.GetUserByIdAsync(userGuid);
        var messageDto = new
        {
            message.Id,
            message.ChatId,
            message.Text,
            message.ImagePath,
            message.SentAt,
            Sender = new
            {
                sender.Id,
                sender.Username,
                sender.ProfilePicturePath
            }
        };

        await Clients.Group(chatId).SendAsync("ReceiveMessage", messageDto);
    }

    public async Task EditMessage(Guid messageId, string newText)
    {
        var message = await _chatService.EditMessageAsync(messageId, newText);
        if (message != null)
        {
            await Clients.Group(message.ChatId).SendAsync("MessageEdited", new
            {
                message.Id,
                message.Text,
                message.EditedAt
            });
        }
    }

    public async Task DeleteMessage(Guid messageId)
    {
        var message = await _chatService.GetMessageByIdAsync(messageId);
        if (message != null)
        {
            var chatId = message.ChatId;
            await _chatService.DeleteMessageAsync(messageId);
            await Clients.Group(chatId).SendAsync("MessageDeleted", messageId);
        }
    }

    public async Task PinMessage(Guid messageId)
    {
        var message = await _chatService.GetMessageByIdAsync(messageId);
        if (message != null)
        {
            await _chatService.PinMessageAsync(messageId);
            await Clients.Group(message.ChatId).SendAsync("MessagePinned", messageId);
        }
    }

    public async Task UnpinMessage(Guid messageId)
    {
        var message = await _chatService.GetMessageByIdAsync(messageId);
        if (message != null)
        {
            await _chatService.UnpinMessageAsync(messageId);
            await Clients.Group(message.ChatId).SendAsync("MessageUnpinned", messageId);
        }
    }

    public async Task NotifyTyping(string chatId, CancellationToken cancellationToken)
    {
        var userId = Context.User?.FindFirst("sub")?.Value;
        if (!string.IsNullOrEmpty(userId))
        {
            var user = await _userService.GetUserPublicInfoByIdAsync(Guid.Parse(userId), cancellationToken);
            await Clients.GroupExcept(chatId, Context.ConnectionId).SendAsync("UserTyping", new
            {
                user.UserId,
                user.Username
            });
        }
    }
}