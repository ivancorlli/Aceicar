using UserContext.Infrastructure.Extension;
using NotificationSystem.Infrastructure.Extension;

public static class ModulesExtensions
{

    public static IServiceCollection InstallModules(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        // Install User Context
        services.InstallUserContextInfrastructure(configuration,environment);
        // Install Notification System
        services.InstallNotificationSystemInfrastructure(configuration);
        return services;
    }

}