using Common.Basis.Error;

namespace CompanyContext.Core.Error;

public record BrandExists:DomainError
{
    public BrandExists() : base(ErrorTypes.TypeBuilder(nameof(CompanyContext),nameof(BrandExists)), "Brand name already used"){}
}

public record BrandNotFound:DomainError
{
    public BrandNotFound() : base(ErrorTypes.TypeBuilder(nameof(CompanyContext),nameof(BrandNotFound)), "Brand not found"){}
}