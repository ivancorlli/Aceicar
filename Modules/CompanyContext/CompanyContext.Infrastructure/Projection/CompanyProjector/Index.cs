using CompanyContext.Application.ViewModel;
using CompanyContext.Core.Event;
using Marten.Events.Aggregation;

namespace CompanyContext.Infrastructure.Projection.CompanyProjector;

public class CompanyProjector : SingleStreamProjection<CompanyProjection>
{
    public static CompanyProjection Create(CompanyCreated @event)
    {
        DateTimeOffset time = DateTimeOffset.UtcNow;
        return new CompanyProjection()
        {
            CompanyId = @event.CompanyId,
            Name = @event.Name,
            Owner = @event.OwnerId,
            CreatedAt = time,
            UpdatedAt = time,
        };
    }
    public void Apply(EmailChanged @event, CompanyProjection company)
    {
        company.Email = @event.Email;
        company.Update();
    }
    public void Apply(LocationChanged @event, CompanyProjection company)
    {
        company.Country = @event.Country;
        company.City = @event.City;
        company.State = @event.State;
        company.PostalCode = @event.PostalCode;
        company.Street = @event.Street;
        company.StreetNumber = @event.StreetNumber;
        if (company.Floor != null && company.Apartment != null)
        {
            company.Floor = @event.Floor;
            company.Apartment = @event.Apartment;
        }
        company.Update();
    }
    public void Apply(PhoneChanged @event, CompanyProjection company)
    {
        company.PhoneCountry = @event.Country;
        company.PhoneNumber = @event.Number;
    }
}