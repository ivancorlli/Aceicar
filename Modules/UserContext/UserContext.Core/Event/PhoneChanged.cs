using UserContext.Core.ValueObject;

namespace UserContext.Core.Event;

public record PhoneChanged(Guid UserId,Phone Phone);