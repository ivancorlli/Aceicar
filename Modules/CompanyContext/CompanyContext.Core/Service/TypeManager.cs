
using Common.Basis.Utils;
using CompanyContext.Core.Error;
using CompanyContext.Core.Repository;

namespace CompanyContext.Core.Service;

public sealed class TypeManager
{
    private ITypeRepository companyTypeRepo;
    public TypeManager(ITypeRepository company)
    {
        companyTypeRepo = company;
    }

    public async Task<Result<Aggregate.Type>> Create(string name)
    {
        bool isUsed = await companyTypeRepo.IsNameUsed(name);
        if(isUsed) return Result.Fail<Aggregate.Type>(new TypeExists());
        Aggregate.Type type = new Aggregate.Type(name);
        return Result.Ok<Aggregate.Type>(type);
    }
    
}