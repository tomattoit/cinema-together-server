using Domain.Entities;
using Application.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Country> Countries { get; set; }

    public DbSet<City> Cities { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
