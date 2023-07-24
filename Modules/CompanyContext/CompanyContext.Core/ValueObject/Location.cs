namespace CompanyContext.Core.ValueObject;

public sealed record Location
{
    public string Country { get; private set; } = default!;
    public string City { get; private set; } = default!;
    public string State { get; private set; } = default!;
    public string PostalCode { get; private set; } = default!;
    public string Steet { get; private set; } = default!;
    public string StreetNumber { get; private set; } = default!;
    public string? Floor { get; private set; } = default!;
    public string? Apartment { get; private set; } = default!;
    private Location() { }
    public static Location Create(
    string country,
    string city,
    string state,
    string postalCode,
    string street,
    string streetNumber
)
    {
        Location newLocation = new();
        newLocation.Country = country.Trim().ToLower();
        newLocation.City = city.Trim().ToLower();
        newLocation.State = state.Trim().ToLower();
        newLocation.PostalCode = postalCode.Trim();
        newLocation.Steet = street.Trim().ToLower();
        newLocation.StreetNumber = streetNumber.Trim();
        return newLocation;
    }
    public static Location Create(
        string country,
        string city,
        string state,
        string postalCode,
        string street,
        string streetNumber,
        string floor,
        string apartment
    )
    {
        Location newLocation = new();
        newLocation.Country = country.Trim().ToLower();
        newLocation.City = city.Trim().ToLower();
        newLocation.State = state.Trim().ToLower();
        newLocation.PostalCode = postalCode.Trim();
        newLocation.Steet = street.Trim().ToLower();
        newLocation.StreetNumber = streetNumber.Trim();
        newLocation.Floor = floor.Trim().ToLower();
        newLocation.Apartment = apartment.Trim().ToLower();
        return newLocation;
    }


}