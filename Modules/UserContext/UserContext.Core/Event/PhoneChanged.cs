using UserContext.Core.ValueObject;

namespace UserContext.Core.Event;

public record PhoneChanged(UserId UserId,Phone Phone);