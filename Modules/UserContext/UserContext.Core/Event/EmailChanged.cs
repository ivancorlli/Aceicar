using UserContext.Core.ValueObject;

namespace UserContext.Core.Event;

public record EmailChanged(Guid UserId,Email Email);