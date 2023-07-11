namespace NotificationSystem.Core.Event.UserEvent;

public sealed record UserCreated(Guid Userid,string Email);
public sealed record EmailChanged(Guid Userid,string Email);
public sealed record PhoneChanged(Guid Userid, string Country,string Number);
public sealed record EmailVerified(Guid Userid,string Code);
public sealed record PhoneVerified(Guid Userid, string Code);
public sealed record ProfileModified(Guid Userid,string Name,string Surname,string Gender,DateTime Birth);
