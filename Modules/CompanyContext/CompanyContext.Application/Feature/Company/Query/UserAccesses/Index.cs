using CompanyContext.Application.Feature.Company.Dto;
using CompanyContext.Application.Interface;
using CompanyContext.Application.ViewModel;
using CompanyContext.Core.Repository;
using Marten;

namespace CompanyContext.Application.Feature.Company.Query.UserAccesses;

public sealed record UserAccessesQuery(Guid UserId);
public static class UserAccessesHandler
{
    public static async Task<IList<UserAccessSummary>> Handle(
        UserAccessesQuery query,
        IEventStoreQuery session,
        IRoleRepository role,
        CancellationToken cancellationToken
    )   
    {
        IList<UserAccessSummary> summary = new List<UserAccessSummary>();
        var data = await session.Query.Query<UserAccess>()
                                .Where(x=>x.Status == Core.Enumerable.AccessStatus.Active)
                                .ToListAsync(cancellationToken);
        foreach (UserAccess item in data)
        {
            string roleName = string.Empty;
            CompanyContext.Core.Aggregate.Role? roleFound = await role.FindById(item.RoleId);
            if(roleFound != null) roleName = roleFound.Name;
            string companyName = string.Empty;
            string? companyPicture = null;
            CompanyProjection? company = await session.Query.Query<CompanyProjection>().Where(x=>x.CompanyId == item.CompanyId).SingleAsync(cancellationToken);
            if(company != null)
            {
                companyName = company.Name;
                companyPicture = company.Picture;
            }
            summary.Add(MapUserAccessSummary.Map(item,roleName,companyName,companyPicture));
        }
        return summary;
    }
}