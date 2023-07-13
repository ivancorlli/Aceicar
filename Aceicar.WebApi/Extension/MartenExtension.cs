using Marten;

public static class MartenExtension
{
    public static IServiceCollection InstallMarten(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddMarten(configuration.GetConnectionString("MessageDb")!)
        // .AddAsyncDaemon(Marten.Events.Daemon.Resiliency.DaemonMode.HotCold)
        // .AssertDatabaseMatchesConfigurationOnStartup()
        // .ApplyAllDatabaseChangesOnStartup()
        // .UseLightweightSessions();
        ;
        return services;
    }   
}