using System;
namespace NotificationSystem.Core.Event.UserEvent;

public sealed record UserCreated(Guid Userid, string Email, string TimeZone);
public sealed record EmailChanged(Guid Userid, string Email, string VerificationCode);
public sealed record PhoneChanged(Guid Userid, string Country, string Number, string VerificationCode);
public sealed record EmailVerified(Guid Userid, string Email, string VerificationCode);
public sealed record PhoneVerified(Guid Userid, string Number, string VerificationCodeCode);
public sealed record ProfileChanged(Guid Userid, string Name, string Surname, string Gender);
public sealed record TimeZoneModified(Guid UserId, string TimeZone);
