using CompanyContext.Application.Interface;
using CompanyContext.Core.Repository;
using CompanyContext.Infrastructure.Context;
using CompanyContext.Infrastructure.Data;
using CompanyContext.Infrastructure.Query;
using CompanyContext.Infrastructure.Repository;
using Marten;
using Marten.Events.Daemon.Resiliency;
using Marten.Services.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Weasel.Core;

namespace CompanyContext.Infrastructure.Extension;

public static class Index
{
    public static IServiceCollection InstallCompanyContextInfrastructure(this IServiceCollection service, IConfiguration configuration)
    {
        service.InstallDb(configuration);
        service.InstallRepository();
        service.InstallQuery();
        return service;
    }

    internal static IServiceCollection InstallDb(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<CompanyDbContext>(x =>
        {
            x.UseNpgsql(configuration.GetConnectionString("CompanyContextDb"));
        });
        service.AddMartenStore<ICompanyStore>(x =>
        {
            x.Connection(configuration.GetConnectionString("CompanyContextDb")!);
            x.UseDefaultSerialization(
            serializerType: SerializerType.SystemTextJson,
            // Optionally override the enum storage
            enumStorage: EnumStorage.AsString,
            // Optionally override the member casing
            casing: Casing.CamelCase
            );
            x.AutoCreateSchemaObjects = Weasel.Core.AutoCreate.CreateOrUpdate;
            x.CompanyOption();
            x.ProductOption();
        })
        .ApplyAllDatabaseChangesOnStartup()
        .AddAsyncDaemon(DaemonMode.HotCold)
        .OptimizeArtifactWorkflow()
        ;
        return service;
    }

    internal static IServiceCollection InstallRepository(this IServiceCollection service)
    {
        service.AddScoped<IEfWork, EfWork>();
        service.AddScoped<IUoW, UoW>();
        service.AddScoped<ITypeRepository, TypeRepository>();
        service.AddScoped<ICategoryRepository, CategoryRepository>();
        service.AddScoped<IServiceRepository, ServiceRepository>();
        service.AddScoped<IBrandRepository, BrandRepository>();
        service.AddScoped<ICompanyRepository, CompanyRepository>();
        service.AddScoped<IRoleRepository, RoleRepository>();
        service.AddScoped<IProductRepository, ProductRepository>();
        return service;
    }
    internal static IServiceCollection InstallQuery(this IServiceCollection services)
    {
        services.AddScoped<IApplicationQuery, EfCoreQuery>();
        services.AddScoped<IEventStoreQuery, MartenQuery>();
        return services;
    }
}