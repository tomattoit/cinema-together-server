using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Data;

public interface IApplicationDbContext
{
    DbSet<City> Cities { get; set; }

    DbSet<Country> Countries { get; set; }

    DbSet<User> Users { get; set; }

    DbSet<UserFriend> UserFriends { get; set; }

    DbSet<EmailVerificationToken> EmailVerificationTokens { get; set; }

    DbSet<Movie> Movies { get; set; }

    DbSet<MovieGenre> MovieGenres { get; set; }

    DbSet<Genre> Genres { get; set; }

    DbSet<MovieReview> MovieReviews { get; set; }

    DbSet<Group> Groups { get; set; }

    DbSet<Chat> Chats { get; set; }

    DbSet<Message> Messages { get; set; }

    DbSet<Poll> Polls { get; set; }

    DbSet<PollOption> PollOptions { get; set; }

    DbSet<Vote> Votes { get; set; }
    
    DbSet<UserGenre> UserGenres { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
