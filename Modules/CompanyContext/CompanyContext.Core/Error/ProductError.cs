using Common.Basis.Error;

namespace CompanyContext.Core.Error;

public record ProductExists:DomainError
{
    public ProductExists() : base(ErrorTypes.TypeBuilder(nameof(CompanyContext.Core),nameof(ProductExists)), "Product already registered"){}
}
public record ImageCountLimit:DomainError
{
    public ImageCountLimit() : base(ErrorTypes.TypeBuilder(nameof(CompanyContext.Core),nameof(ImageCountLimit)), "This product has reached image count limit"){}
}