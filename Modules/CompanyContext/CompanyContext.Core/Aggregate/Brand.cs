
using CompanyContext.Core.Event;
using CompanyContext.Core.Interface;

namespace CompanyContext.Core.Aggregate;

public sealed class Brand : IBrand
{

    internal static Brand Create(BrandCreated @event)
    {
        Brand brand = new()
        {
            Id = @event.BrandId,
            Name = @event.Name.Trim().ToLower(),
            Status = @event.Status,
        };
        if (@event.Logo != null)
        {
            brand.Logo = @event.Logo;
        }
        return brand;
    }

    internal static Brand Create(BrandForCompanyCreated @event)
    {
        Brand brand = new()
        {
            Id = @event.BrandId,
            Name = @event.Name.Trim().ToLower(),
            Status = @event.Status,
            CompanyId = @event.CompanyId
        };
        if (@event.Logo != null)
        {
            brand.Logo = @event.Logo;
        }
        return brand;
    }
}