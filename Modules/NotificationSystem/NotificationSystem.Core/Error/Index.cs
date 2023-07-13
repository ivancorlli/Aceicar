using Common.Basis.Error;

namespace NotificationSystem.Core.Error;



public record UserNotFound : DomainError
{
    public UserNotFound() : base(ErrorTypes.TypeBuilder(nameof(NotificationSystem),nameof(UserNotFound)), "User not found"){}
}

public record InvalidCode : DomainError
{
    public InvalidCode(string code) : base(ErrorTypes.TypeBuilder(nameof(NotificationSystem),nameof(InvalidCode)), $"Code {code} is not valid"){}
}

public record PhoneNotFound : DomainError
{
    public PhoneNotFound() : base(ErrorTypes.TypeBuilder(nameof(NotificationSystem),nameof(PhoneNotFound)), $"User not have a valid phone"){}
}