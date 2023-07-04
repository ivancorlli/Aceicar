namespace UserContext.Core.ValueObject;

public record Username
{
    public string Value {get; private set;} = default!;
    public static Username Create(string username)
    {
        Username newUsername = new();
        newUsername.Value = username.Trim().ToLower();
        return newUsername;
    }
}