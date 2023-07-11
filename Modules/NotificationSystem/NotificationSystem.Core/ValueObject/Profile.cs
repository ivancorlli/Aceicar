namespace NotificationSystem.Core.ValueObject;

public record Profile
{
    public string Name { get; private set; } = default!;
    public string Surname { get; private set; } = default!;
    public string Gender { get; private set; } = default!;
    public DateTime Birth { get; private set; }
    public static Profile Create(string name, string surname, string gender, DateTime birth)
    {
        return new Profile()
        {
            Name = name.Trim().ToLower(),
            Surname = surname.Trim().ToLower(),
            Gender = gender,
            Birth = birth
        };
    }

}