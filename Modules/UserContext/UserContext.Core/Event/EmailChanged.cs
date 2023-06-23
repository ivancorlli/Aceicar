using UserContext.Core.ValueObject;

namespace UserContext.Core.Event;

public record EmailChanged(UserId UserId,Email Email);