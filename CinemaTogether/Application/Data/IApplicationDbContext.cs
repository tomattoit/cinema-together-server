using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Data;

public interface IApplicationDbContext
{
    DbSet<City> Cities { get; }

    DbSet<Country> Countries { get; }
    
    DbSet<User> Users { get; }

    DbSet<UserFriend> UserFriends { get; }
    
    DbSet<EmailVerificationToken> EmailVerificationTokens { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
