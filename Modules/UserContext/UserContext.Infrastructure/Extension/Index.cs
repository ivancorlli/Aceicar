using Marten;
using Marten.Events;
using Marten.Services.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UserContext.Application.Service;
using UserContext.Core.Repository;
using UserContext.Core.Service;
using UserContext.Infrastructure.Data;
using UserContext.Infrastructure.Repository;
using UserContext.Infrastructure.Service;
using Weasel.Core;

namespace UserContext.Infrastructure.Extension;
public static class Index
{

    public static IServiceCollection InstallUserContextInfrastructure(this IServiceCollection service, IConfiguration configuration, IHostEnvironment enviroment)
    {
        service.InstallDb(configuration, enviroment);
        service.InstallRepository();
        return service;
    }

    internal static IServiceCollection InstallDb(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        services.AddSingleton<IDocumentStore>(sp =>
        {
            var connectionString = configuration.GetConnectionString("UserContextDb")!;
            var documentStore = DocumentStore.For(options =>
            {
                options.Connection(connectionString);
                options.ConfigureUser();
            });
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