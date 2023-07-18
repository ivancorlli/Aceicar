using CompanyContext.Core.Enumerable;

namespace CompanyContext.Core.Event;

public sealed record BrandCreated(Guid BrandId,string Name, string? Logo,BrandStatus Status = BrandStatus.Active);
public sealed record BrandForCompanyCreated(Guid BrandId,string Name,Guid CompanyId,string? Logo, BrandStatus Status = BrandStatus.Active);
public sealed record BrandDeactivated(Guid BrandId, BrandStatus Status=BrandStatus.Inactive);
public sealed record BrandLogoChanged(Guid BrandId,string Logo);