using IdContext.Web.Options;

namespace IdContext.Web.Extension;

public static class IdContextWebExtension
{
    public static IServiceCollection InstallIdWeb(this IServiceCollection services,IConfiguration configuration)
    {
        services.Configure<RedirectUrlOptions>(opts => configuration.GetSection("RedirectUrl").Bind(opts));
        return services;
    }
    
}