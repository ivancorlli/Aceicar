using CompanyContext.Core.Repository;
using CompanyContext.Infrastructure.Context;
using CompanyContext.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyContext.Infrastructure.Extension;

public static class Index
{
    public static IServiceCollection  InstallCompanyContextInfrastructure(this IServiceCollection service, IConfiguration configuration)
    {
        service.InstallDb( configuration);
        service.InstallRepository();
        return service;
    }
    
    internal static IServiceCollection InstallDb(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<CompanyDbContext>(x=>{
            x.UseNpgsql(configuration.GetConnectionString("CompanyContextDb"));
        });
        return service;
    }

    internal static IServiceCollection InstallRepository(this IServiceCollection service)
    {
        service.AddScoped<IEfWork,EfWork>();
        service.AddScoped<ITypeRepository,TypeRepository>();
        service.AddScoped<ICategoryRepository,CategoryRepository>();
        service.AddScoped<IServiceRepository, ServiceRepository>();
        return service;
    }
}