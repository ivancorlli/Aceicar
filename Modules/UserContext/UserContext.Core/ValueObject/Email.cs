namespace UserContext.Core.ValueObject;

public record Email
{
    public string Value {get;private set;} = default!;
    private Email(){}

    public static Email Create(string Value)
    {
        Email newEmail = new();
        newEmail.Value = Value.Trim().ToLower();
        return newEmail;
    }
}