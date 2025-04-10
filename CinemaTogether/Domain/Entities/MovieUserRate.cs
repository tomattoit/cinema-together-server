using Domain.Common;

namespace Domain.Entities;

public class MovieUserRate
{
    public Guid MovieId { get; set; }
    
    public Movie Movie { get; set; }
    
    public Guid UserId { get; set; }
    
    public User User { get; set; }
    
    public decimal Rate { get; set; }
}