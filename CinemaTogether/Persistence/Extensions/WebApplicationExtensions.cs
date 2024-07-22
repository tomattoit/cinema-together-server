using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence.Extensions;

public static class WebApplicationExtensions
{
    public static void SeedDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var appDbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
        DatabaseSeeder.SeedData(appDbContext);
    }
}
