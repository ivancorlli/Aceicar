using Common.Basis.Utils;
using CompanyContext.Core.Aggregate;
using CompanyContext.Core.Constant;
using CompanyContext.Core.Entity;
using CompanyContext.Core.Error;
using CompanyContext.Core.Event;
using CompanyContext.Core.Repository;
using CompanyContext.Core.ValueObject;

namespace CompanyContext.Core.Service;

public sealed class CompanyManager
{
    private readonly ICompanyRepository _company;
    private readonly IRoleRepository _role;
    private readonly ITypeRepository _type;
    public CompanyManager(
        ICompanyRepository repo,
        IRoleRepository role,
        ITypeRepository type
    )   
    {
        _company = repo;
        _role = role;
        _type = type;
    }
    public async Task<Result<Company>> Create(CompanyCreated @event,Guid typeId,Guid specializationId)
    {
        bool isUsed = await _company.IsNameUsed(@event.Name);
        if(isUsed) return Result.Fail<Company>(new CompanyExists());
        bool isValid = await IsValidSpecialization(typeId,specializationId);
        if(!isValid) return Result.Fail<Company>(new SpecializationNotFound());
        Company newCompany = Company.Create(@event);
        Result<CompanyArea> area = newCompany.AddToArea(typeId,specializationId);
        if(area.IsFailure) return Result.Fail<Company>(area.Error);
        Role? role = await _role.FindByName(Roles.Admin);
        if(role == null) return Result.Fail<Company>(new RoleNotFound());
        Result<Access> access = newCompany.AddAccess(@event.OwnerId,role.Id);
        if(access.IsFailure) return Result.Fail<Company>(access.Error);
        return Result.Ok(newCompany);
    }
    private async Task<bool> IsValidSpecialization(Guid typeId,Guid specialzationId)
    {
        Specialization? specialization = await _type.FindById(typeId,specialzationId);
        if(specialization != null) return true;
        return false;
    }
    
}