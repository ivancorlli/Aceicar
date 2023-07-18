using Common.Basis.Error;
namespace CompanyContext.Core.Error;

public record TypeExists:DomainError
{
    public TypeExists() : base(ErrorTypes.TypeBuilder(nameof(CompanyContext),nameof(TypeExists)), "Type name already used"){}
}

public record TypeNotFound:DomainError
{
    public TypeNotFound() : base(ErrorTypes.TypeBuilder(nameof(CompanyContext),nameof(TypeNotFound)), "Type not found"){}
}

public record SpecializationNotFound:DomainError
{
    public SpecializationNotFound() : base(ErrorTypes.TypeBuilder(nameof(CompanyContext),nameof(SpecializationNotFound)), "Specialization not found"){}
}