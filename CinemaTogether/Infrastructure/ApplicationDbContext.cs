using Domain.Entities;
using Application.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
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
        
        modelBuilder.Entity<Role>().HasData(
            new Role
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Name = "Admin"
            },
            new Role
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Name = "User"
            });
        
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
                Gender = Domain.Enums.Gender.Male,
                RoleId = Guid.Parse("22222222-2222-2222-2222-222222222222")
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
                Gender = Domain.Enums.Gender.Male,
                RoleId = Guid.Parse("11111111-1111-1111-1111-111111111111")
            });

        base.OnModelCreating(modelBuilder);
    }
}
