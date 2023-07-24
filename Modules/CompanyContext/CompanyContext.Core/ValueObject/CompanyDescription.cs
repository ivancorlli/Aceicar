namespace CompanyContext.Core.ValueObject;

public sealed record CompanyDescription
{
    public string Value {get;private set;} = default!;
    public static CompanyDescription Create (string description)
    {
        return new CompanyDescription()
        {
            Value = description.Trim()
        };
    }
}