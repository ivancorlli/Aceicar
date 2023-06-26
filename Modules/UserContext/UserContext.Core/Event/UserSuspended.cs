using UserContext.Core.Enumerable;
using UserContext.Core.ValueObject;

namespace UserContext.Core.Event;

public sealed record UserSuspended(Guid UserId, Status Status);