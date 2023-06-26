using UserContext.Core.ValueObject;

namespace UserContext.Core.Event;

public record UsernameChanged(Guid UserId, Username Username);
