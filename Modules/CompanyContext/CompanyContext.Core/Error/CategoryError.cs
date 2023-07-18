using Common.Basis.Error;

namespace CompanyContext.Core.Error;
public record CategoryExists:DomainError
{
    public CategoryExists() : base(ErrorTypes.TypeBuilder(nameof(CompanyContext),nameof(CategoryExists)), "Category name already used"){}
}
public record CategoryNotFound:DomainError
{
    public CategoryNotFound() : base(ErrorTypes.TypeBuilder(nameof(CompanyContext),nameof(CategoryNotFound)), "Category not found"){}
}