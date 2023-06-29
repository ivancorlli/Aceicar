using Marten;
using Marten.Events;
using Marten.Services.Json;
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
        service.InstallDb(configuration, enviroment);
        service.InstallRepository();
        return service;
    }

    internal static IServiceCollection InstallDb(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        services.AddMarten(opts =>
        {
            opts.Connection(configuration.GetConnectionString("UserContextDb")!);
            opts.Events.StreamIdentity = StreamIdentity.AsGuid;

            opts.UseDefaultSerialization(
                serializerType:SerializerType.Newtonsoft ,
                // Optionally override the enum storage
                enumStorage: EnumStorage.AsString,
                // Optionally override the member casing
                casing: Casing.CamelCase
            );

            opts.AutoCreateSchemaObjects = AutoCreate.CreateOrUpdate;
            opts.ConfigureUser();
        })
        .UseLightweightSessions()
        .ApplyAllDatabaseChangesOnStartup()
        .AddAsyncDaemon(Marten.Events.Daemon.Resiliency.DaemonMode.HotCold)

        ;
        return services;
    }

    internal static IServiceCollection InstallRepository(this IServiceCollection services)
    {
        services.AddScoped<IUoW, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<UserManager>();
        return services;
    }

}