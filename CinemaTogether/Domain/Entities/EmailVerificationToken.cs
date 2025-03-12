using Domain.Common;

namespace Domain.Entities;

public class EmailVerificationToken : IBaseEntity
{
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }
    
    public User User { get; set; }
}