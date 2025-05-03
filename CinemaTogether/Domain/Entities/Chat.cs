using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Chat
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public DateTime CreatedAt { get; set; }

        // Навигационные свойства
        public Group Group { get; set; }
        public ICollection<Message> Messages { get; set; }

        public Chat()
        {
            Messages = new List<Message>();
        }
    }
}