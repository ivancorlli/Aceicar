using Microsoft.AspNetCore.DataProtection;
using MuverProvider.Helper;

public static class KeyExtension
{
    public static IServiceCollection InstallKey(this IServiceCollection services,IHostEnvironment environment)
    {
        var path = Path.Combine(environment.ContentRootPath, "./Key");
        services.AddSingleton<DevKeys>();
        services.AddDataProtection()
                .SetApplicationName("IDP")
                .PersistKeysToFileSystem(new DirectoryInfo(path))
                .SetDefaultKeyLifetime(TimeSpan.FromDays(14));

        return services;
    }

}