using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Group
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string Type { get; set; }
        public Guid OwnerId { get; set; }
        public string ChatId { get; set; }

        // Навигационные свойства
        public User Owner { get; set; }
        public Chat Chat { get; set; }
        public ICollection<User> Members { get; set; }
        public ICollection<Genre> PreferredGenres { get; set; }
        public ICollection<Poll> Polls { get; set; }

        public Group()
        {
            Members = new List<User>();
            PreferredGenres = new List<Genre>();
            Polls = new List<Poll>();
        }
    }
}