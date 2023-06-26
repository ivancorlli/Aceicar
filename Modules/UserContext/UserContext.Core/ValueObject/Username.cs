namespace UserContext.Core.ValueObject;

public record Username
{
    public string Value {get; private set;}
    private Username(string username)
    {
        Value = username;
    }

    public static Username Create(string username)
    {
        Username newUsername = new(username);
        return newUsername;
    }
}