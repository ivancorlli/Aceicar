
using Common.Basis.Utils;
using CompanyContext.Core.Aggregate;
using CompanyContext.Core.Error;
using CompanyContext.Core.Repository;

namespace CompanyContext.Core.Service;

public sealed class TypeManager
{
    private ICompanyTypeRepository companyTypeRepo;
    public TypeManager(ICompanyTypeRepository company)
    {
        companyTypeRepo = company;
    }

    public async Task<Result<CompanyType>> Create(string name)
    {
        bool isUsed = await companyTypeRepo.IsNameUsed(name);
        if(isUsed) return Result.Fail<CompanyType>(new CompanyTypeExists());
        CompanyType type = new CompanyType(name);
        return Result.Ok<CompanyType>(type);
    }
    
}