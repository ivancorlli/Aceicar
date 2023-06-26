using UserContext.Core.ValueObject;

namespace UserContext.Core.Event;

public sealed record ImageChanged(Guid UserId,ProfileImage Image);