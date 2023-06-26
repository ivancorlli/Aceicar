using Common.Basis.Error;

namespace UserContext.Core.Error;

public record UserExists : DomainError
{
    public UserExists() : base($"{nameof(UserContext)}.{nameof(UserExists)}", "User already registered")
    {
    }
}

public record UserNotFound : DomainError
{
    public UserNotFound() : base($"{nameof(UserContext)}.{nameof(UserNotFound)}", "User not found")
    {
    }
}
