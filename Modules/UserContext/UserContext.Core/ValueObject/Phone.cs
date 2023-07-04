namespace UserContext.Core.ValueObject;

public record Phone
{
    public string PhoneCountry {get;private set;} =default!;
    public string PhoneNumber {get;private set;} = default!;
    public static Phone Create(string phoneCountry,string phoneNumber)
    {
        return new Phone() {
            PhoneCountry = phoneCountry.Trim().ToUpper(),
            PhoneNumber = phoneNumber.Trim()
        };
    }
}