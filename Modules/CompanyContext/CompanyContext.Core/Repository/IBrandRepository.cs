using CompanyContext.Core.Aggregate;

namespace CompanyContext.Core.Repository;

public interface IBrandRepository
{
    Task<bool> IsNameUsed(string Name);
    Task<ProductBrand?> GetById(Guid BrandId);
    void Create(ProductBrand Root);
    
}