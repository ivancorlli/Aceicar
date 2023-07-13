namespace NotificationSystem.Core.ValueObject;

public record Profile
{
    public string Name { get; private set; } = default!;
    public string Surname { get; private set; } = default!;
    public string Gender { get; private set; } = default!;
    public static Profile Create(string name, string surname, string gender)
    {
        return new Profile()
        {
            Name = name.Trim().ToLower(),
            Surname = surname.Trim().ToLower(),
            Gender = gender,
        };
    }

}