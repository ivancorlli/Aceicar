using Common.Basis.Repository;

namespace CompanyContext.Core.Repository;

public interface IUoW:IUnitOfWork
{
    public IBrandRepository BrandRepository {get;}

    
}