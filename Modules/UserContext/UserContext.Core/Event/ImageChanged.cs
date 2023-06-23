using UserContext.Core.ValueObject;

namespace UserContext.Core.Event;

public sealed record ImageChanged(UserId UserId,ProfileImage Image);