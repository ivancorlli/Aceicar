using Common.Basis.Aggregate;
using CompanyContext.Core.Enumerable;
using CompanyContext.Core.Event;

namespace CompanyContext.Core.Interface;

public abstract class IBrand : IAggregate
{
    public string Name { get; protected set; } = default!;
    public BrandStatus Status { get; protected set; } = default!;
    public string? Logo { get; protected set; }
    /// <summary>
    /// If Company Id is not null means that this brand is valid only for that company. Is not native in the system 
    /// </summary>
    /// <value></value>
    public Guid? CompanyId { get; protected set; }

    public void Deactivate()
    {
        BrandDeactivated @event = new(Id);
        Apply(@event);
        Raise(@event);
    }

    public void ChangeLogo(string logo)
    {
        BrandLogoChanged @event = new(Id, logo);
        Apply(@event);
        Raise(@event);
    }

    public void Apply(BrandDeactivated @event)
    {
        Status = @event.Status;
    }
    public void Apply(BrandLogoChanged @event)
    {
        Logo = @event.Logo;
    }
}