namespace CompanyContext.Core.Repository;

public interface IEfWork
{
    public ICompanyTypeRepository CompanyTypeRepository {get;}
    public IBrandRepository BrandRepository {get;}
    public Task SaveChangesAsync(CancellationToken cancellationToken);

    
}