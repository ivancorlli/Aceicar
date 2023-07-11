using UserContext.Core.Enumerable;

namespace UserContext.Core.Event.UserEvent;


public sealed record UserCreated(Guid UserId,string Email,string TimeZone,Status Status = Status.Active);
public sealed record EmailChanged(Guid UserId,string Email);
public sealed record ImageChanged(Guid UserId,string Image);
public sealed record PhoneChanged(Guid UserId,string PhoneCountry,string PhoneNumber);
public sealed record UsernameChanged(Guid UserId, string Username);
public sealed record UserSuspended(Guid UserId, Status Status=Status.Suspended);
public sealed record ProfileModified(Guid UserId, string Name,string Surname,string Gender, DateTime Birth);
public sealed record LocationModified(Guid UserId,string Country,string City,string State,string PostalCode, LocationStatus Status);
public sealed record TimeZoneModified(Guid UserId,string TimeZone);