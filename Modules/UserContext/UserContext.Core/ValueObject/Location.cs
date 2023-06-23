namespace UserContext.Core.ValueObject;

public record Location
{
    public string Country {get;private set;} = default!;
    public string City {get;private set;} = default!;
    public string State {get;private set;} = default!;
    public string PostalCode {get;private set;} = default!;
    
    public Location Create(
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

}