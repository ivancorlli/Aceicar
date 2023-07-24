using CompanyContext.Application.ViewModel;

namespace CompanyContext.Application.Feature.Company.Dto;

public sealed record AccessByIdRole(Guid RoleId, string Name);
public sealed record AccessByIdDto
{
    public Guid AccessId { get; set; } = default!;
    public Guid CompanyId { get; set; } = default!;
    public AccessByIdRole Role { get; set; } = default!;
}

public static class MapAccessById
{
    public static AccessByIdDto Map(UserAccess access, string roleName)
    {
        return new AccessByIdDto()
        {
            AccessId = access.AccessId,
            CompanyId = access.CompanyId,
            Role = new AccessByIdRole(access.RoleId,roleName)
        };
    }
}