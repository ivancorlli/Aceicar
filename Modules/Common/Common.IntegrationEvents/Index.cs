namespace Common.IntegrationEvents;

public sealed record UserCreatedEvent(Guid UserId,string Email,string TimeZone);
public sealed record UserEmailChangedEvent(Guid UserId, string Email);
public sealed record UserPhoneChangedEvent(Guid UserId, string Country, string Number);
public sealed record UserProfileChangedEvent(Guid UserId, string Name,string Surname, string Gender);
public sealed record UserTimeZoneModifiedEvent(Guid UserId,string TimeZone);