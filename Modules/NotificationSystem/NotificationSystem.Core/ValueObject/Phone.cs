namespace NotificationSystem.Core.ValueObject;

public record Phone
{
    public string Country { get; private set; } = default!;
    public string Number { get; private set; } = default!;
    public bool Verified { get; protected set; } = false;
    public string? VerificationCode { get; protected set; } = default!;
    public DateTime? VerifiedAt { get; protected set; } = default!;
    public static Phone Create(string phoneCountry, string phoneNumber)
    {
        return new Phone()
        {
            Country = phoneCountry.Trim().ToUpper(),
            Number = phoneNumber.Trim()
        };
    }
    public string GenerateVerificationCode()
    {
        VerificationCode = new Random(18).Next(0, 1000000).ToString("D6");
        return VerificationCode;
    }
    public void Verify(string code)
    {
        if (code == VerificationCode)
        {
            Verified = true;
        }
        else
        {
            Verified = false;
        }
    }
}