using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Application.Common.Constants;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public static class ServiceCollectionExtensions
{
    public static void ConfigureSqlServerDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(ConfigSectionNames.DefaultConnectionString);
        services.AddDbContext<ApplicationDbContext>(options => 
            options.UseSqlServer(connectionString));
    }
}
