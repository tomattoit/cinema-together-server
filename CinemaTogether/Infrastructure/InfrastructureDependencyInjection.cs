using System.Text;
using Application.Common.Auth;
using Application.Common.Constants;
using Application.Common.Services;
using Application.Data;
using Domain.Constants;
using Infrastructure.Auth;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration) =>
        services
            .AddDatabase(configuration)
            .AddAuthenticationInternal(configuration)
            .AddAuthorization()
            .AddServices();

    private static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(ConfigSectionNames.DefaultConnectionString);
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        
        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

        return services;
    }

    private static IServiceCollection AddAuthenticationInternal(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!)),
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    ClockSkew = TimeSpan.Zero
                };

                o.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if (context.Request.Cookies.TryGetValue(CommonConstants.DefaultTokenName, out var token))
                        {
                            context.Token = token;
                        }

                        return Task.CompletedTask;
                    }
                };
            });
        
        services.AddSingleton<ITokenProvider, TokenProvider>();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services
            .AddScoped<ILoginService, LoginService>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<ICountryService, CountryService>()
            .AddScoped<IFriendService, FriendService>()
            .AddScoped<IEmailSender, EmailSender>()
            .AddScoped<IMovieService, MovieService>();
        
        return services;
    }
}
