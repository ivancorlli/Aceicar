namespace CompanyContext.Core.ValueObject;

public sealed record CompanyPicture
{
    public string Value {get; private set;} = default!;
    public static CompanyPicture Create(string picture)
    {
        return new CompanyPicture()
        {
            Value = picture.Trim()
        };
    }
    
}