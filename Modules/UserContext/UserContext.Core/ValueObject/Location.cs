using UserContext.Core.Enumerable;

namespace UserContext.Core.ValueObject;

public record Location
{
    public string Country { get; private set; } = default!;
    public string City { get; private set; } = default!;
    public string State { get; private set; } = default!;
    public string PostalCode { get; private set; } = default!;
    public string? Steet {get;private set;} = default!;
    public int? StreetNumber {get; private set;} =default!;
    public LocationStatus Status { get; private set; }
    private Location() { }
    public static Location Create(
        string country,
        string city,
        string state,
        string postal,
        LocationStatus status
    )
    {
        Location newLocation = new();
        newLocation.Country = country.Trim();
        newLocation.City = city.Trim();
        newLocation.State = state.Trim();
        newLocation.PostalCode = postal.Trim();
        newLocation.Status = status;
        return newLocation;
    }

}