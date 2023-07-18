using Common.Basis.Utils;
using CompanyContext.Core.Error;
using CompanyContext.Core.Repository;

namespace CompanyContext.Core.Service;

public sealed class ServiceManager
{
    private readonly IServiceRepository repository;
    public ServiceManager(IServiceRepository repo)
    {
        repository = repo;
    }
    
    public async Task<Result<Aggregate.Service>> Create(string name)
    {
        bool isUsed = await repository.IsNameUsed(name);
        if(isUsed) return Result.Fail<Aggregate.Service>(new ServiceExists());
        Aggregate.Service service = new(name);
        return Result.Ok(service);
    }
}