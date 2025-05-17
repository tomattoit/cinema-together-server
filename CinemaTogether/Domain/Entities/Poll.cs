using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Poll
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public Guid GroupId { get; set; }
        public Guid EventId { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Description { get; set; }
        
        public Group Group { get; set; }
        public User Creator { get; set; }
        public Event Event { get; set; }
        public ICollection<PollOption> Options { get; set; } = new List<PollOption>();
    }
}