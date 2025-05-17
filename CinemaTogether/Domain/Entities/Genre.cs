using Domain.Common;

namespace Domain.Entities;

public class Genre : IBaseEntity
{
    public Guid Id { get; set; }
    
    public int ApiId { get; set; }

    public string Name { get; set; }

    public ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();
    
    public ICollection<UserGenre> UserGenres { get; set; } = new List<UserGenre>();
}