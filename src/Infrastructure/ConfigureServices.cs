using Application.Interfaces;
using Domain.Exceptions;
using Infrastructure.Data;
using Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database") ?? throw new InvalidOperationException("Connection string 'Database' not found.");
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddDatabaseDeveloperPageExceptionFilter();

        services
            .AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddGoogle(options =>
            {
                options.ClientId = GetConfigurationValue(configuration, "Authentication:Google:ClientId");
                options.ClientSecret = GetConfigurationValue(configuration, "Authentication:Google:ClientSecret");
            });

        AddRepositories(services);

        return services;
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddTransient<ITravelPlanRepository, TravelPlanRepository>();
    }

    private static string GetConfigurationValue(IConfiguration configuration, string key)
    {
        return configuration[key] ?? throw new MissingConfigurationSetting(key);
    }
}