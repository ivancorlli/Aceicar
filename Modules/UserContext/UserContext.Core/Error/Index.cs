using Common.Basis.Error;
using Common.Basis.ValueObject;
using UserContext.Core.ValueObject;

namespace UserContext.Core.Error;

public record UserExists : DomainError
{
    public UserExists() : base(ErrorTypes.TypeBuilder(nameof(UserContext.Core),nameof(UserExists)), "User already registered"){}
}

public record UserNotFound : DomainError
{
    public UserNotFound() : base(ErrorTypes.TypeBuilder(nameof(UserContext.Core),nameof(UserNotFound)), "User not found"){}
}

public record EmailAlreadyUsed : DomainError
{
    public EmailAlreadyUsed(Email email) : base(ErrorTypes.TypeBuilder(nameof(UserContext.Core),nameof(EmailAlreadyUsed)), $"Email {email.Value} already used"){}
}
public record PhoneAlreadyUsed : DomainError
{
    public PhoneAlreadyUsed(Phone phone) : base(ErrorTypes.TypeBuilder(nameof(UserContext.Core),nameof(PhoneAlreadyUsed)), $"Phone {phone.Number} already used"){}
}
public record UsernameAlreadyUsed : DomainError
{
    public UsernameAlreadyUsed(Username username) : base(ErrorTypes.TypeBuilder(nameof(UserContext.Core),nameof(UsernameAlreadyUsed)), $"Username {username.Value} already used"){}
}

public record InvalidGender : DomainError
{
    public InvalidGender() : base(ErrorTypes.TypeBuilder(nameof(UserContext.Core),nameof(InvalidGender)), $"Must select a valid gender Male or Female "){}
}