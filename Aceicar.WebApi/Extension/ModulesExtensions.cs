
using IdContext.Infrastructure.Extension;

public static class ModulesExtensions
{

    public static IServiceCollection InstallModules(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        // Install Id Context
        services.InstallIdInfrastructure(configuration,environment);
        return services;
    }

}