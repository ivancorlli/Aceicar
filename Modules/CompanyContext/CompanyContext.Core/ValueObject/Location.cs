namespace CompanyContext.Core.ValueObject;

public sealed record Location
{
    public string Country { get; private set; } = default!;
    public string City { get; private set; } = default!;
    public string State { get; private set; } = default!;
    public string PostalCode { get; private set; } = default!;
    public string Steet {get;private set;} = default!;
    public string StreetNumber {get; private set;} =default!;
    public string? Floor {get;private set;} = default!;
    public string? Apartment {get; private set;} = default!;
    private Location() { }
    public static Location Create(
        string country,
        string city,
        string state,
        string postal,
        string street,
        string streetNumber
    )
    {
        Location newLocation = new();
        newLocation.Country = country.Trim().ToLower();
        newLocation.City = city.Trim().ToLower();
        newLocation.State = state.Trim().ToLower();
        newLocation.PostalCode = postal.Trim();
        newLocation.Steet = street.Trim().ToLower();
        newLocation.StreetNumber = streetNumber.Trim();
        return newLocation;
    }

}