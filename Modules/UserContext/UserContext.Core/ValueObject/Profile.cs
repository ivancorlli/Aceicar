namespace UserContext.Core.ValueObject;

public record Profile
{
    public string Name {get;private set;} = default!;
    public string Surname {get;private set;} = default!;
    public string Gender {get; private set;} = default!;
    public DateTime Birth   {get;private set;}

}