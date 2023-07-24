using Oakton.Resources;
using Wolverine;
using Wolverine.EntityFrameworkCore;
using Wolverine.Postgresql;

public static class WolverineExtension
{

    public static IServiceCollection InstallWolverine(this IServiceCollection services, IHostBuilder host, IConfiguration configuration, IHostEnvironment env)
    {

        // WOLVERINE MESSAGE HUB
        host.UseWolverine(o =>
        {
            // Setting up Postgresql-backed message storage
            // This requires a reference to Wolverine.Postgresql
            o.PersistMessagesWithPostgresql(configuration.GetConnectionString("MessageDb")!);

            // Set up Entity Framework Core as the support
            // for Wolverine's transactional middleware
            o.UseEntityFrameworkCoreTransactions();

            o.OptimizeArtifactWorkflow();

            o.Policies.UseDurableLocalQueues();
            o.Policies.UseDurableInboxOnAllListeners();
            o.Policies.UseDurableOutboxOnAllSendingEndpoints();

            // Instalar Assemblies
            // - IntegrationEvents
            o.Discovery.IncludeAssembly(typeof(Common.IntegrationEvents.UserCreatedEvent).Assembly);

            // - NotificationSystem
            o.Discovery.IncludeAssembly(typeof(NotificationSystem.Application.EventHandler.UserCreatedHandler).Assembly);
            o.Discovery.IncludeAssembly(typeof(NotificationSystem.Infrastructure.Extension.Index).Assembly);

            // - UserContext
            o.Discovery.IncludeAssembly(typeof(UserContext.Application.Feature.User.Command.CreateUser.CreateUserHandler).Assembly);
            o.Discovery.IncludeAssembly(typeof(UserContext.Infrastructure.Extension.Index).Assembly);

            // - Company
            o.Discovery.IncludeAssembly(typeof(CompanyContext.Infrastructure.Extension.Index).Assembly);
            o.Discovery.IncludeAssembly(typeof(CompanyContext.Application.Feature.Category.Command.CreateCategory.CreateCategoryHandler).Assembly);
            o.Discovery.IncludeAssembly(typeof(CompanyContext.Api.Controller.Category.AddToSpecialization).Assembly);
            
        });

        services.AddResourceSetupOnStartup();
        // This is rebuilding the persistent storage database schema on startup
        // and also clearing any persisted envelope stat
        host.UseResourceSetupOnStartup();
        return services;
    }


}