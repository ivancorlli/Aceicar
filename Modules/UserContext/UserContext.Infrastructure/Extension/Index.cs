using Marten;
using Marten.Events.Daemon.Resiliency;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UserContext.Application.Interface;
using UserContext.Core.Repository;
using UserContext.Core.Service;
using UserContext.Infrastructure.Data;
using UserContext.Infrastructure.Query;
using UserContext.Infrastructure.Repository;

namespace UserContext.Infrastructure.Extension;
public static class Index
{

    public static IServiceCollection InstallUserContextInfrastructure(this IServiceCollection service, IConfiguration configuration, IHostEnvironment enviroment)
    {
        service.AddMartenStore<IUserStore>(x=>{
            x.Connection(configuration.GetConnectionString("UserContextDb")!);
            x.UserContext();
        })
        .ApplyAllDatabaseChangesOnStartup()
        .AddAsyncDaemon(DaemonMode.HotCold)
        .OptimizeArtifactWorkflow()
        ;
        service.InstallQuery();
        service.InstallRepository();
        return service;
    }
    internal static IServiceCollection InstallRepository(this IServiceCollection services)
    {
        services.AddScoped<IUoW, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<UserManager>();

        return services;
    }

    internal static IServiceCollection InstallQuery(this IServiceCollection services)
    {
        services.AddScoped<IApplicationQuery,EventStoreQuery>();
        return services;
    }

}