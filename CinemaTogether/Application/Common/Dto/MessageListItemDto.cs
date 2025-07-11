namespace Application.Common.Dto;

public class MessageListItemDto
{
    public DateTime SentAt { get; set; }
    public Guid SenderId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public string? ImagePath { get; set; }
} 