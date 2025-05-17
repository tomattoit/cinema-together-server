using Domain.Entities;

namespace Application.Common.Services;

public interface IChatService
{
    Task<Chat> CreateChatAsync(string type, CancellationToken cancellationToken = default);
    Task<Chat> GetChatByIdAsync(string chatId, CancellationToken cancellationToken = default);
    Task<Message> GetMessageByIdAsync(Guid messageId, CancellationToken cancellationToken = default);
    Task<Message> SendMessageAsync(string chatId, Guid senderId, string text, string imagePath = null, CancellationToken cancellationToken = default);
    Task<Message> EditMessageAsync(Guid messageId, string newText, CancellationToken cancellationToken = default);
    Task DeleteMessageAsync(Guid messageId, CancellationToken cancellationToken = default);
    Task PinMessageAsync(Guid messageId, CancellationToken cancellationToken = default);
    Task UnpinMessageAsync(Guid messageId, CancellationToken cancellationToken = default);
    Task<List<Message>> GetChatMessagesAsync(string chatId, int skip = 0, int take = 50, CancellationToken cancellationToken = default);
    Task<List<Message>> GetPinnedMessagesAsync(string chatId, CancellationToken cancellationToken = default);
}