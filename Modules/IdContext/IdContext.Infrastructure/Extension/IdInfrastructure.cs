using IdContext.Core.Entity;
using IdContext.Infrastructure.Context;
using IdContext.Infrastructure.Options;
using IdContext.Infrastructure.Seed;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdContext.Infrastructure.Extension;

public static class IdInfrastructure
{

    public static IServiceCollection InstallIdInfrastructure(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
    {
        services.InstallDb(configuration);
        services.InstallInterfaces();
        services.InstallIdentity();
        services.InstallExternalAuth(configuration);
        services.InstallOpenid(env, configuration);
        services.InstallHostedServices(configuration);
        return services;
    }

    internal static IServiceCollection InstallDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(o =>
        {
            o.UseNpgsql(
                configuration.GetConnectionString("IdDb"),
                x => x.EnableRetryOnFailure()
                );
            o.UseOpenIddict();
        });

        return services;
    }
    internal static IServiceCollection InstallInterfaces(this IServiceCollection services)
    {
        return services;
    }
    internal static IServiceCollection InstallIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, Role>(o =>
        {
            o.SignIn.RequireConfirmedAccount = true;
            // password config
            o.Password.RequireDigit = true;
            o.Password.RequireNonAlphanumeric = false;
            o.Password.RequiredLength = 6;
            o.Password.RequireLowercase = true;
            o.Password.RequireUppercase = true;
            // user config
            o.User.RequireUniqueEmail = true;
            o.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789._@";
        })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
    internal static IServiceCollection InstallExternalAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication()
            .AddGoogle(
                opts =>
                {
                    opts.ClientId = configuration["Authentication:Google:ClientId"]!;
                    opts.ClientSecret = configuration["Authentication:Google:Secret"]!;
                    opts.SignInScheme = IdentityConstants.ExternalScheme;
                })
            .AddFacebook(opts =>
                {
                    opts.ClientId = configuration["Authentication:Facebook:ClientId"]!;
                    opts.ClientSecret = configuration["Authentication:Facebook:Secret"]!;
                    opts.SignInScheme = IdentityConstants.ExternalScheme;
                });

        return services;
    }
    internal static IServiceCollection InstallHostedServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddHostedService<SeedRoles>();
        services.Configure<ProviderClientOptions>(opts => configuration.GetSection("ProviderClient").Bind(opts));
        // Seed Data
        services.AddHostedService<SeedProviderApi>();
        return services;
    }

}