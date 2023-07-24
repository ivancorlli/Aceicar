namespace CompanyContext.Core.Event;

public sealed record CompanyCreated(Guid CompanyId, Guid OwnerId, string Name);
public sealed record AddedToArea(Guid CompanyId, Guid TypeId, Guid SpecializationId);
public sealed record CompanyPublished(Guid CompanyId, DateTime PublishAt, bool Publish = true);
public sealed record EmailChanged(Guid CompanyId, string Email);
public sealed record PhoneChanged(Guid CompanyId, string Country, string Number);
public sealed record PictureChanged(Guid CompanyId, string Picture);
public sealed record DescriptionChanged(Guid CompanyId, string Description);
public sealed record LocationChanged(Guid CompanyId, string Country, string City, string State, string PostalCode, string Street, string StreetNumber, string? Floor, string? Apartment);
public sealed record EmployeeCreated(Guid CompanyId,Guid EmployeeId, Guid UserId);
public sealed record EmployeeDeactivated(Guid CompanyId, Guid UserId);
public sealed record EmployeeDeleted(Guid CompanyId, Guid UserId);
public sealed record AccessCreated(Guid CompanyId,Guid AccessId,Guid UserId,Guid RoleId);