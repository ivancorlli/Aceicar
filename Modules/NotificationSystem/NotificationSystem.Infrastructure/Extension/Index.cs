using Marten;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NotificationSystem.Core.Repository;
using NotificationSystem.Infrastructure.Repository;

namespace NotificationSystem.Infrastructure.Extension;
public static class Index
{

    public static IServiceCollection InstallNotificationSystemInfrastructure(this IServiceCollection service, IConfiguration configuration, IHostEnvironment enviroment)
    {
        service.InstallDb(configuration);
        service.InstallRepository();
        return service;
    }

    internal static IServiceCollection InstallDb(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddSingleton<IDocumentStore>(sp =>
                {
                    var connectionString = configuration.GetConnectionString("NotificationSystemDb")!;
                    var documentStore = DocumentStore.For(x =>
                    {
                        x.Connection(connectionString);
                    });
                    documentStore.LightweightSession("notificationsystemdb");
                    return documentStore;

                });

        return services;
    }

    internal static IServiceCollection InstallRepository(this IServiceCollection services)
    {
        services.AddScoped<IUoW, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }

}