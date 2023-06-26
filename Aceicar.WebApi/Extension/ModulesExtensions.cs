using UserContext.Infrastructure.Extension;

public static class ModulesExtensions
{

    public static IServiceCollection InstallModules(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        // Install User Context
        services.InstallUserContextInfrastructure(configuration,environment);
        return services;
    }

}