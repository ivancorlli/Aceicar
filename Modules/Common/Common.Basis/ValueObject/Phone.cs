namespace Common.Basis.ValueObject;

public record Phone
{
    public string Country { get; private set; } = default!;
    public string Number { get; private set; } = default!;
    public static Phone Create(string phoneCountry, string phoneNumber)
    {
        return new Phone()
        {
            Country = phoneCountry.Trim().ToUpper(),
            Number = phoneNumber.Trim()
        };
    }
}