namespace UserContext.Core.ValueObject;

public class TimeZone
{
    public string Country {get;private set;} = default!;
    public string Time {get;private set;} = default!;

    public static TimeZone Create(string country,string time)
    {
        TimeZone zone = new();
        zone.Country = country.Trim().ToUpper();
        zone.Time = time.Trim().ToUpper();
        return zone;
    }
    
}