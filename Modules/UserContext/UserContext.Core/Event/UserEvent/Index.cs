using UserContext.Core.Enumerable;

namespace UserContext.Core.Event.UserEvent;


public sealed record UserCreated(Guid UserId,string Email,Status Status = Status.Active);
public sealed record EmailChanged(Guid UserId,string Email);
public sealed record ImageChanged(Guid UserId,string Image);
public sealed record PhoneChanged(Guid UserId,string PhoneCountry,string PhoneNumber);
public sealed record UsernameChanged(Guid UserId, string Username);
public sealed record UserSuspended(Guid UserId, Status Status=Status.Suspended);