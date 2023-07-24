namespace CompanyContext.Core.ValueObject;

public sealed record CompanyName
{
    public string Name {get;private set;} = default!;
    public static CompanyName Create(string name)
    {
        return new CompanyName()
        {
            Name = name.Trim().ToLower()
        };
    }
}