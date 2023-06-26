using Marten;
using Marten.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UserContext.Core.Repository;
using UserContext.Core.Service;
using UserContext.Infrastructure.Data;
using UserContext.Infrastructure.Repository;
using Weasel.Core;

namespace UserContext.Infrastructure.Extension;
public static class Index
{

    public static IServiceCollection InstallUserContextInfrastructure(this IServiceCollection service, IConfiguration configuration, IHostEnvironment enviroment)
    {
        service.InstallDb(configuration,enviroment);
        service.InstallRepository();
        return service;
    }

    internal static IServiceCollection InstallDb(this IServiceCollection services, IConfiguration configuration,IHostEnvironment environment)
    {
        services.AddMarten(opts =>
        {
            opts.Connection(configuration.GetConnectionString("UserContextDb")!);
            opts.DatabaseSchemaName = "UserContext";
            opts.Events.StreamIdentity = StreamIdentity.AsGuid;

            if (environment.IsDevelopment())
            {
                opts.AutoCreateSchemaObjects = AutoCreate.All;
            }

            // Configure data Schemas 
            opts.ConfigureUser();
        })
        .UseLightweightSessions()
        .ApplyAllDatabaseChangesOnStartup()
        ;
        return services;
    }

    internal static IServiceCollection InstallRepository(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<UserManager>();
        return services;
    }

}