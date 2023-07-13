using Common.Basis.Error;
namespace CompanyContext.Core.Error;

public record CompanyTypeExists:DomainError
{
    public CompanyTypeExists() : base(ErrorTypes.TypeBuilder(nameof(CompanyContext),nameof(CompanyTypeExists)), "Type name already used"){}
}

public record CompanyTypeNotFound:DomainError
{
    public CompanyTypeNotFound() : base(ErrorTypes.TypeBuilder(nameof(CompanyContext),nameof(CompanyTypeNotFound)), "Type not found"){}
}

public record SpecializationNotFound:DomainError
{
    public SpecializationNotFound() : base(ErrorTypes.TypeBuilder(nameof(CompanyContext),nameof(SpecializationNotFound)), "Specialization not found"){}
}