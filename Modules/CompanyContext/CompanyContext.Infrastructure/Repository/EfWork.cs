using CompanyContext.Core.Repository;
using CompanyContext.Infrastructure.Context;

namespace CompanyContext.Infrastructure.Repository;

public class EfWork : IEfWork
{
    public ITypeRepository TypeRepository{get;private set;}
    public ICategoryRepository CategoryRespository {get;private set;}
    public IServiceRepository ServiceRepository {get;private set;}
    public CompanyDbContext Context;

    public EfWork(
        ITypeRepository companyTypeRepository,
        ICategoryRepository categoryRepository,
        IServiceRepository serviceRepository,
        CompanyDbContext context
    )
    {
        TypeRepository = companyTypeRepository;
        CategoryRespository = categoryRepository;
        ServiceRepository = serviceRepository;
        Context = context;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await Context.SaveChangesAsync(cancellationToken);
    }
}