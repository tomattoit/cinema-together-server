using Domain.Common;

namespace Domain.Entities;

public class Event : IBaseEntity
{
    public Guid Id { get; set; }
    
    public DateTime Date { get; set; }
    
    public string Place { get; set; }
    
    public Movie Movie { get; set; }
    
    public Guid MovieId { get; set; }
    
    public Group Group { get; set; }
    
    public Guid GroupId { get; set; }
    
    public ICollection<Poll> Polls { get; set; }
}