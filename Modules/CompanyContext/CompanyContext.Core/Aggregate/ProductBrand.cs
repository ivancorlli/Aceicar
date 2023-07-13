using CompanyContext.Core.Enumerable;

namespace CompanyContext.Core.Aggregate;

public sealed class ProductBrand
{
    public Guid Id {get; private set;}
    public string Name {get;private set;} = default!;
    public string? BrandIcon {get;private set;} = default!;
    public BrandStatus Status {get;private set;} = default!;

    internal ProductBrand(string name)
    {
        Id = Guid.NewGuid();
        Name = name.Trim().ToLower();
        Status = BrandStatus.Active;
    }

    public void ChangeIcon(string icon)
    {
        BrandIcon = icon.Trim();
    }

    public void Activate()
    {
        Status = BrandStatus.Active;
    }

    public void Deactivate()
    {
        Status = BrandStatus.Inactive;
    }

    internal void Delete()
    {
        Status = BrandStatus.Deleted;
    }

}