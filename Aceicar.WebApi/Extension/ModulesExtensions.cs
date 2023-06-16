
using IdContext.Infrastructure.Extension;
using IdContext.Web.Extension;

public static class ModulesExtensions
{

    public static IServiceCollection InstallModules(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        // Install Id Context
        services.InstallIdInfrastructure(configuration,environment);
        services.InstallIdWeb(configuration);
        return services;
    }

}