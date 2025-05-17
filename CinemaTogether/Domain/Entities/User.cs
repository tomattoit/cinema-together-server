using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class User : IBaseEntity
{
    public Guid Id { get; set; }

    public required string Email { get; set; }

    public required string PasswordHash { get; set; }

    public required string Username { get; set; }
    
    public bool IsEmailVerified { get; set; }

    public bool TwoFactorEnabled { get; set; }
    
    public string Name { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public Gender? Gender { get; set; }

    public string ProfilePicturePath { get; set; }

    public decimal Rating { get; set; }

    public int RatingCount { get; set; }

    public Guid? CityId { get; set; }

    public City City { get; set; }
    
    public Role Role { get; set; }
    
    public List<UserFriend> Friends { get; set; }
    
    public List<UserFriend> FriendOf { get; set; }
    
    public List<Movie> RatedMovies { get; set; }

    public List<MovieReview> MovieUserRates { get; set; }
    
    public ICollection<UserGenre> UserGenres { get; set; } = new List<UserGenre>();
}
