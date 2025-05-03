using System;

namespace Domain.Entities
{
    public class Vote
    {
        public Guid Id { get; set; }
        public Guid PollOptionId { get; set; }
        public Guid UserId { get; set; }

        // Навигационные свойства
        public PollOption PollOption { get; set; }
        public User User { get; set; }
    }
}