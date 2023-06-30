using Common.Basis.Error;
using UserContext.Core.ValueObject;

namespace UserContext.Core.Error;

public record UserExists : DomainError
{
    public UserExists() : base(ErrorTypes.TypeBuilder(nameof(UserContext),nameof(UserExists)), "User already registered"){}
}

public record UserNotFound : DomainError
{
    public UserNotFound() : base(ErrorTypes.TypeBuilder(nameof(UserContext),nameof(UserNotFound)), "User not found"){}
}

public record EmailAlreadyUsed : DomainError
{
    public EmailAlreadyUsed(Email email) : base(ErrorTypes.TypeBuilder(nameof(UserContext),nameof(EmailAlreadyUsed)), $"Email {email.Value} already used"){}
}
public record PhoneAlreadyUsed : DomainError
{
    public PhoneAlreadyUsed(Phone phone) : base(ErrorTypes.TypeBuilder(nameof(UserContext),nameof(PhoneAlreadyUsed)), $"Phone {phone.PhoneNumber} already used"){}
}
public record UsernameAlreadyUsed : DomainError
{
    public UsernameAlreadyUsed(Username username) : base(ErrorTypes.TypeBuilder(nameof(UserContext),nameof(UsernameAlreadyUsed)), $"Username {username.Value} already used"){}
}