
using CompanyContext.Application.ViewModel;

namespace CompanyContext.Application.Feature.Company.Dto;

public sealed record UserAccessCompany(Guid CompanyId,string Name, string? Picture);
public sealed record UserAccessRole(Guid RoleId,string Name);
public sealed record UserAccessSummary
{
    public Guid AccessId { get; set; }
    public UserAccessRole Role {get;set;} = default!;
    public UserAccessCompany Company {get;set;} = default!;
}

public static class MapUserAccessSummary
{
    public static UserAccessSummary Map(UserAccess summary, string roleName, string companyName, string? companyPicture)
    {
        return new UserAccessSummary()
        {
            AccessId = summary.AccessId,
            Company = new UserAccessCompany(summary.CompanyId,companyName,companyPicture),
            Role = new UserAccessRole(summary.RoleId,roleName)

        };
    }
}