using System;

namespace Domain.Entities
{
    public class Message
    {
        public Guid Id { get; set; }
        public string ChatId { get; set; }
        public Guid SenderId { get; set; }
        public string Text { get; set; }
        public bool IsPinned { get; set; }
        public DateTime SentAt { get; set; }
        public DateTime? EditedAt { get; set; }
        public string ImagePath { get; set; }

        // Навигационные свойства
        public Chat Chat { get; set; }
        public User Sender { get; set; }
    }
}