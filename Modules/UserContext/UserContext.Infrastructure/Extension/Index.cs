using Marten;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UserContext.Application.Service;
using UserContext.Core.Repository;
using UserContext.Core.Service;
using UserContext.Infrastructure.Data;
using UserContext.Infrastructure.Repository;
using UserContext.Infrastructure.Service;

namespace UserContext.Infrastructure.Extension;
public static class Index
{

    public static IServiceCollection InstallUserContextInfrastructure(this IServiceCollection service, IConfiguration configuration, IHostEnvironment enviroment)
    {
        service.InstallDb(configuration);
        service.InstallRepository();
        return service;
    }

    internal static IServiceCollection InstallDb(this IServiceCollection services,IConfiguration configuration)
    {
        
        services.AddSingleton<IDocumentStore>(sp =>
        {
            var connectionString = configuration.GetConnectionString("UserContextDb")!;
            var documentStore = DocumentStore.For(x=>{
                x.Connection(connectionString);
                x.ConfigureUser();
            });

            documentStore.LightweightSession("usercontextdb");
            return documentStore;

        });

        return services;
    }

    internal static IServiceCollection InstallRepository(this IServiceCollection services)
    {
        services.AddScoped<IUoW, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<UserManager>();

        // appliaction services
        services.AddScoped<IUserAccountService, UserAccountService>();
        return services;
    }

}