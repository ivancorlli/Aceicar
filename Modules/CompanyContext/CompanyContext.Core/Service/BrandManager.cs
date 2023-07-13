using Common.Basis.Utils;
using CompanyContext.Core.Aggregate;
using CompanyContext.Core.Error;
using CompanyContext.Core.Repository;

namespace CompanyContext.Core.Service;

public sealed class BrandManager
{
    private readonly IBrandRepository brandRepository;
    public BrandManager(IBrandRepository repository)
    {
        brandRepository = repository;
    }

    public async Task<Result<ProductBrand>> CreateBrand(string name)
    {
        bool isUsed = await brandRepository.IsNameUsed(name);
        if(isUsed) return Result.Fail<ProductBrand>(new BrandExists());
        ProductBrand newBrand = new ProductBrand(name);
        return Result.Ok(newBrand); 
    }
}