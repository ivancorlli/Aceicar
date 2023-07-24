namespace UserContext.Core.ValueObject;

public record Location
{
    public string Country { get; private set; } = default!;
    public string City { get; private set; } = default!;
    public string State { get; private set; } = default!;
    public string PostalCode { get; private set; } = default!;
    public string? Steet {get;private set;} = default!;
    public string? StreetNumber {get; private set;} =default!;
    private Location() { }
    public static Location Create(
        string country,
        string city,
        string state,
        string postal
    )
    {
        Location newLocation = new();
        newLocation.Country = country.Trim();
        newLocation.City = city.Trim();
        newLocation.State = state.Trim();
        newLocation.PostalCode = postal.Trim();
        return newLocation;
    }
    
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
        newLocation.Country = country.Trim();
        newLocation.City = city.Trim();
        newLocation.State = state.Trim();
        newLocation.PostalCode = postal.Trim();
        newLocation.Steet = street.Trim();
        newLocation.StreetNumber = streetNumber.Trim();
        return newLocation;
    }

}