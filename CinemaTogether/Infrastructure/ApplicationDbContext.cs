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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = Guid.Parse("A0440A78-41CC-419C-B05F-B511EE65D28A"),
                Email = "d.krumkachev@gmail.com",
                Username = "Test1",
                PasswordHash = Hasher.Hash("A-123123"),
                TwoFactorEnabled = false,
                Name = "Robby Krieger",
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
                Name = "John Densmore",
                DateOfBirth = DateTime.Now.AddYears(-23),
                Gender = Gender.Male,
                Role = Role.Admin
            });

        base.OnModelCreating(modelBuilder);
    }
}
