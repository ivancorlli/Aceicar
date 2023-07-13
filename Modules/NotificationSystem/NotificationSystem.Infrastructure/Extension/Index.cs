using Marten;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotificationSystem.Core.Repository;
using NotificationSystem.Infrastructure.Data;
using NotificationSystem.Infrastructure.Repository;
namespace NotificationSystem.Infrastructure.Extension;
public static class Index
{

    public static IServiceCollection InstallNotificationSystemInfrastructure(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddMartenStore<INotficiationSystem>(x=>{
            x.Connection(configuration.GetConnectionString("NotificationSystemDb")!);
            x.NotificationSystem();
        })
        .ApplyAllDatabaseChangesOnStartup()
        .AddAsyncDaemon(Marten.Events.Daemon.Resiliency.DaemonMode.HotCold)
        .OptimizeArtifactWorkflow();
        service.InstallRepository();
        return service;
    }

    internal static IServiceCollection InstallRepository(this IServiceCollection services)
    {
        services.AddScoped<IUoW, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }

}