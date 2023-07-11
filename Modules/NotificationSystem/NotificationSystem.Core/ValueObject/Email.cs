namespace NotificationSystem.Core.ValueObject;

public record Email
{
    public string Value {get;private set;} = default!;
    public bool Verified {get;protected set;} = false;
    public string? VerificationCode {get;protected set;} = default!;
    public DateTime? VerifiedAt {get;protected set;} = default!;
    public static Email Create(string Value)
    {
        Email newEmail = new();
        newEmail.Value = Value.Trim().ToLower();
        return newEmail;
    }

    internal static Email CreateWithVerify(string Value)
    {
        Email newEmail = new();
        newEmail.Value = Value.Trim().ToLower();
        newEmail.Verified = true;
        newEmail.VerifiedAt = DateTime.Now;
        return newEmail;
    }
    public string GenerateVerificationCode()
    {
        VerificationCode = new Random(18).Next(0, 1000000).ToString("D6");
        return VerificationCode;
    }
    internal void Verify(string code)
    {
        if(code == VerificationCode)
        {
            Verified = true;
        }else {
            Verified = false;
        }
    }
}