using Oakton.Resources;
using Wolverine;
using Wolverine.EntityFrameworkCore;
using Wolverine.Postgresql;


namespace Aceicar.WebApi.Extension;

public static class WolverineExtension
{

    public static IServiceCollection InstallWolverine(this IServiceCollection services, IHostBuilder host, IConfiguration configuration)
    {

        host.UseWolverine(o =>
        {
            // Setting up Postgresql-backed message storage
            // This requires a reference to Wolverine.Postgresql
            o.PersistMessagesWithPostgresql(configuration.GetConnectionString("MessageDb")!);

            // Set up Entity Framework Core as the support
            // for Wolverine's transactional middleware
            o.UseEntityFrameworkCoreTransactions();

            // This forces every outgoing subscriber to use durable
            // messaging
            o.Policies.UseDurableOutboxOnAllSendingEndpoints();
            // Make every single listener endpoint use
            // durable message storage 
            o.Policies.UseDurableInboxOnAllListeners();
            o.OptimizeArtifactWorkflow();
            // Add the auto transaction middleware attachment policy. With this option, you will no longer need to decorate handler methods with the [Transactional] attribute.
            o.Policies.AutoApplyTransactions();

            // o.Services.InstallUserContextInfrastructure(configuration);

            // Instalar Assemblies
            // - NotificationSystem/Application
            o.Discovery.IncludeAssembly(typeof(NotificationSystem.Application.Command.UserCreatedEvent.UserCreatedHandler).Assembly);

            // - UserContext/Application
            o.Discovery.IncludeAssembly(typeof(UserContext.Application.Feature.ApplicationUser.Command.CreateUser.CreateUserHandler).Assembly);
        });

        // This is rebuilding the persistent storage database schema on startup
        // and also clearing any persisted envelope stat
        host.UseResourceSetupOnStartup();

        return services;
    }


}