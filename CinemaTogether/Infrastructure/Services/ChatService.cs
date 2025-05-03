using Application.Common.Interfaces;
using Application.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class ChatService : IChatService
{
    private readonly IApplicationDbContext _context;

    public ChatService(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Chat> CreateChatAsync(string type, CancellationToken cancellationToken = default)
    {
        var chat = new Chat
        {
            Id = Guid.NewGuid().ToString(),
            Type = type,
            CreatedAt = DateTime.UtcNow
        };

        _context.Chats.Add(chat);
        await _context.SaveChangesAsync(cancellationToken);
        return chat;
    }

    public async Task<Chat> GetChatByIdAsync(string chatId, CancellationToken cancellationToken = default)
    {
        return await _context.Chats
            .Include(c => c.Messages)
            .FirstOrDefaultAsync(c => c.Id == chatId, cancellationToken);
    }

    public async Task<Message> SendMessageAsync(string chatId, Guid senderId, string text, string imagePath = null, CancellationToken cancellationToken = default)
    {
        var message = new Message
        {
            ChatId = chatId,
            SenderId = senderId,
            Text = text,
            ImagePath = imagePath,
            SentAt = DateTime.UtcNow
        };

        _context.Messages.Add(message);
        await _context.SaveChangesAsync(cancellationToken);
        return message;
    }

    public async Task<Message> EditMessageAsync(Guid messageId, string newText, CancellationToken cancellationToken = default)
    {
        var message = await _context.Messages.FindAsync(new object[] { messageId }, cancellationToken);
        if (message != null)
        {
            message.Text = newText;
            message.EditedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync(cancellationToken);
        }
        return message;
    }

    public async Task DeleteMessageAsync(Guid messageId, CancellationToken cancellationToken = default)
    {
        var message = await _context.Messages.FindAsync(new object[] { messageId }, cancellationToken);
        if (message != null)
        {
            _context.Messages.Remove(message);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task PinMessageAsync(Guid messageId, CancellationToken cancellationToken = default)
    {
        var message = await _context.Messages.FindAsync(new object[] { messageId }, cancellationToken);
        if (message != null)
        {
            message.IsPinned = true;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task UnpinMessageAsync(Guid messageId, CancellationToken cancellationToken = default)
    {
        var message = await _context.Messages.FindAsync(new object[] { messageId }, cancellationToken);
        if (message != null)
        {
            message.IsPinned = false;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<List<Message>> GetChatMessagesAsync(string chatId, int skip = 0, int take = 50, CancellationToken cancellationToken = default)
    {
        return await _context.Messages
            .Include(m => m.Sender)
            .Where(m => m.ChatId == chatId)
            .OrderByDescending(m => m.SentAt)
            .Skip(skip)
            .Take(take)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Message>> GetPinnedMessagesAsync(string chatId, CancellationToken cancellationToken = default)
    {
        return await _context.Messages
            .Include(m => m.Sender)
            .Where(m => m.ChatId == chatId && m.IsPinned)
            .OrderByDescending(m => m.SentAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<Message> GetMessageByIdAsync(Guid messageId, CancellationToken cancellationToken = default)
    {
        return await _context.Messages
            .Include(m => m.Sender)
            .FirstOrDefaultAsync(m => m.Id == messageId, cancellationToken);
    }
}