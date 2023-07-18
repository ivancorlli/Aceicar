namespace CompanyContext.Core.Repository;

public interface IEfWork
{
    public ITypeRepository TypeRepository {get;}
    public ICategoryRepository CategoryRespository {get;}
    public IServiceRepository ServiceRepository {get;}
    public Task SaveChangesAsync(CancellationToken cancellationToken);

    
}