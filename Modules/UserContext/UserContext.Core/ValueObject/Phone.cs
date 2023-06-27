namespace UserContext.Core.ValueObject;

public record Phone
{
    public string PhoneCountry {get;private set;} =default!;
    public string PhoneNumber {get;private set;} = default!;

    private Phone(){}

    private Phone(string country,string number)
    {
        PhoneCountry = country.Trim().ToUpper();
        PhoneNumber = number.Trim();
    }

    public static Phone Create(string phoneCountry,string phoneNumber)
    {
        return new Phone(phoneCountry,phoneNumber);
    }
}