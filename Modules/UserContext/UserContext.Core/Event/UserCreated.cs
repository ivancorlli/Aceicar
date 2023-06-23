using UserContext.Core.Enumerable;
using UserContext.Core.ValueObject;

namespace UserContext.Core.Event;

public record UserCreated(UserId UserId,Email Email,Status Status);