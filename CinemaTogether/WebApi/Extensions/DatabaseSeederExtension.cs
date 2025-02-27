using Application.Data;
using Infrastructure;

namespace WebApi.Extensions;

public static class DatabaseSeederExtension
{
    public static void SeedDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var appDbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
        var seeder = new DatabaseSeeder(appDbContext);
        seeder.Seed();
    }
}