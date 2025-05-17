using Domain.Entities;
using Application.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Domain.Enums;
using Shared.Cryptography;

namespace Infrastructure;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options), IApplicationDbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Country> Countries { get; set; }

    public DbSet<City> Cities { get; set; }

    public DbSet<UserFriend> UserFriends { get; set; }

    public DbSet<EmailVerificationToken> EmailVerificationTokens { get; set; }

    public DbSet<Movie> Movies { get; set; }

    public DbSet<MovieGenre> MovieGenres { get; set; }

    public DbSet<Genre> Genres { get; set; }

    public DbSet<MovieReview> MovieReviews { get; set; }

    public DbSet<Group> Groups { get; set; }

    public DbSet<Chat> Chats { get; set; }

    public DbSet<Message> Messages { get; set; }

    public DbSet<Poll> Polls { get; set; }

    public DbSet<PollOption> PollOptions { get; set; }

    public DbSet<Vote> Votes { get; set; }
    
    public DbSet<UserGenre> UserGenres { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<UserFriend>()
            .HasKey(uf => new { uf.UserId, uf.FriendId });

        modelBuilder.Entity<UserFriend>()
            .HasOne(uf => uf.User)
            .WithMany(u => u.Friends)
            .HasForeignKey(uf => uf.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<UserFriend>()
            .HasOne(uf => uf.Friend)
            .WithMany(u => u.FriendOf)
            .HasForeignKey(uf => uf.FriendId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<MovieGenre>()
            .HasKey(mg => new { mg.MovieId, mg.GenreId });

        modelBuilder.Entity<MovieGenre>()
            .HasOne(mg => mg.Movie)
            .WithMany(m => m.MovieGenres)
            .HasForeignKey(mg => mg.MovieId);

        modelBuilder.Entity<MovieGenre>()
            .HasOne(mg => mg.Genre)
            .WithMany(g => g.MovieGenres)
            .HasForeignKey(fg => fg.GenreId);

        modelBuilder.Entity<MovieReview>()
            .HasKey(mr => mr.Id);
        
        modelBuilder.Entity<MovieReview>()
            .HasOne(mr => mr.Movie)
            .WithMany(m => m.MovieReviews)
            .HasForeignKey(mr => mr.MovieId);
        
        modelBuilder.Entity<MovieReview>()
            .HasOne(mr => mr.User)
            .WithMany(u => u.MovieUserRates)
            .HasForeignKey(mr => mr.UserId);
        
        modelBuilder.Entity<UserGenre>()
            .HasKey(mg => new { mg.UserId, mg.GenreId });
        
        modelBuilder.Entity<UserGenre>()
            .HasOne(mg => mg.User)
            .WithMany(u => u.UserGenres)
            .HasForeignKey(mg => mg.UserId);
        
        modelBuilder.Entity<UserGenre>()
            .HasOne(mg => mg.Genre)
            .WithMany(g => g.UserGenres)
            .HasForeignKey(mg => mg.GenreId);
        
        modelBuilder.Entity<Poll>()
            .HasOne(p => p.Event)
            .WithMany(e => e.Polls)
            .HasForeignKey(p => p.EventId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Genre>().HasData(
            new Genre { Id = Guid.Parse("048291de-5df2-43cb-9b59-ddcc7ba2f3d4"), ApiId = 28, Name = "Action" },
            new Genre { Id = Guid.Parse("1b33dcf2-e1ad-4a02-bd6a-9b874d6369cd"), ApiId = 12, Name = "Adventure" },
            new Genre { Id = Guid.Parse("27f6e407-e3d5-4b34-a713-a4da1a661604"), ApiId = 16, Name = "Animation" },
            new Genre { Id = Guid.Parse("cd4ab5ce-61e7-4c0e-8cb1-c527250375ca"), ApiId = 35, Name = "Comedy" },
            new Genre { Id = Guid.Parse("99116751-059a-4c22-8981-7479b7fb0e0e"), ApiId = 80, Name = "Crime" },
            new Genre { Id = Guid.Parse("881dbfa8-5503-47cb-9545-dd2ebc94e850"), ApiId = 99, Name = "Documentary" },
            new Genre { Id = Guid.Parse("ad48a3ca-bf42-4dcf-896a-d2a0934297be"), ApiId = 18, Name = "Drama" },
            new Genre { Id = Guid.Parse("44d1426b-e51b-4605-ada6-5eb5d868c357"), ApiId = 10751, Name = "Family" },
            new Genre { Id = Guid.Parse("2ac7342a-f220-479e-9a7b-18afada9fbbe"), ApiId = 14, Name = "Fantasy" },
            new Genre { Id = Guid.Parse("0bd2dcfd-0cfa-4449-929f-748c399c8dbf"), ApiId = 36, Name = "History" },
            new Genre { Id = Guid.Parse("3489587e-6fa3-4790-8216-be5bbb3532bc"), ApiId = 27, Name = "Horror" },
            new Genre { Id = Guid.Parse("a4cd390f-39fd-462d-9b9e-7ff3640f7135"), ApiId = 10402, Name = "Music" },
            new Genre { Id = Guid.Parse("c3007078-1f8c-4f34-9986-0be2cb6e6306"), ApiId = 9648, Name = "Mystery" },
            new Genre { Id = Guid.Parse("1cb76074-a2e9-4e50-bb64-165c93533598"), ApiId = 10749, Name = "Romance" },
            new Genre { Id = Guid.Parse("3af9901c-a0cc-44c4-928b-e478617f9ff1"), ApiId = 878, Name = "Science Fiction" },
            new Genre { Id = Guid.Parse("45998a9a-0a9c-4986-9e0f-05faeb3b5932"), ApiId = 10770, Name = "TV Movie" },
            new Genre { Id = Guid.Parse("f015ce95-992a-4197-b86c-de5599ebbc98"), ApiId = 53, Name = "Thriller" },
            new Genre { Id = Guid.Parse("b504bd64-5df9-4546-b7e4-ffad637318be"), ApiId = 10752, Name = "War" },
            new Genre { Id = Guid.Parse("ba415ac9-bc10-4652-99a1-fbf6e92beb4b"), ApiId = 37, Name = "Western" });

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = Guid.Parse("A0440A78-41CC-419C-B05F-B511EE65D28A"),
                Email = "d.krumkachev@gmail.com",
                Username = "Test1",
                PasswordHash = Hasher.Hash("A-123123"),
                TwoFactorEnabled = false,
                Name = "Robby Krieger",
                IsEmailVerified = true,
                DateOfBirth = DateTime.Now.AddYears(-25),
                Gender = Gender.Male,
                Role = Role.User
            },
            new User
            {
                Id = Guid.Parse("DDC7D332-E194-4E6E-A77D-C1EBCE29E746"),
                Email = "artemij1258@gmail.com",
                Username = "Test2",
                PasswordHash = Hasher.Hash("A-123123"),
                TwoFactorEnabled = false,
                IsEmailVerified = true,
                Name = "John Densmore",
                DateOfBirth = DateTime.Now.AddYears(-23),
                Gender = Gender.Male,
                Role = Role.Admin
            });

        base.OnModelCreating(modelBuilder);
    }
}
