using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class PollOption
    {
        public Guid Id { get; set; }
        public Guid PollId { get; set; }
        public string Text { get; set; }

        // Навигационные свойства
        public Poll Poll { get; set; }
        public ICollection<Vote> Votes { get; set; }

        public PollOption()
        {
            Votes = new List<Vote>();
        }
    }
}