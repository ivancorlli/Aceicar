using Common.Basis.Utils;
using CompanyContext.Core.Aggregate;
using CompanyContext.Core.Error;
using CompanyContext.Core.Event;
using CompanyContext.Core.Repository;

namespace CompanyContext.Core.Service;

public sealed class BrandManager
{
    private readonly IBrandRepository brandRepository;
    public BrandManager(IBrandRepository repository)
    {
        brandRepository = repository;
    }

    public async Task<Result<Brand>> Create(BrandCreated @event)
    {
        bool isUsed = await brandRepository.IsNameUsed(@event.Name);
        if(isUsed) return Result.Fail<Brand>(new BrandExists());
        Brand newBrand = Brand.Create(@event);
        return Result.Ok(newBrand); 
    }
        public async Task<Result<Brand>> Create(BrandForCompanyCreated @event)
    {
        bool isUsed = await brandRepository.IsNameUsed(@event.Name);
        if(isUsed) return Result.Fail<Brand>(new BrandExists());
        Brand newBrand = Brand.Create(@event);
        return Result.Ok(newBrand); 
    }
}