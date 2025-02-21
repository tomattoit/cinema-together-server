using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class User : IBaseEntity
{
    public Guid Id { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public string Username { get; set; }

    public bool TwoFactorEnabled { get; set; }
    
    public string Name { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public Gender? Gender { get; set; }

    public string ProfilePicturePath { get; set; }

    public decimal Rating { get; set; }

    public int RatingCount { get; set; }

    public Guid? CityId { get; set; }

    public City City { get; set; }
    
    public Guid? RoleId { get; set; }
    
    public Role Role { get; set; }
}
