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
    
    DbSet<Movie> Movies { get; }
    
    DbSet<MovieGenre> MovieGenres { get; }
    
    DbSet<Genre> Genres { get; }
    
    DbSet<MovieReview> MovieReviews { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
