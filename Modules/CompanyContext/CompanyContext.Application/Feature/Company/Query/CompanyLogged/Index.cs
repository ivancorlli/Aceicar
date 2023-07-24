using Common.Basis.Interface;
using Common.Basis.Utils;
using CompanyContext.Application.Feature.Company.Dto;
using CompanyContext.Application.Interface;
using CompanyContext.Application.ViewModel;
using CompanyContext.Core.Error;
using Marten;

namespace CompanyContext.Application.Feature.Company.Query.CompanyLogged;

public sealed record CompanyLoggedQuery(Guid AccessId);
public static class CompanyLoggedHandler
{
    public static async Task<IOperationResult<CompanyLoggedDto>> Handle(
        CompanyLoggedQuery query,
        IEventStoreQuery session,
        CancellationToken cancellationToken
    )
    {
        IReadOnlyList<UserAccess> access = await session.Query.Query<UserAccess>().Where(x => x.AccessId == query.AccessId).ToListAsync();
        if (access.Count > 0)
        {
            UserAccess accessLogged = access.First();
            IReadOnlyList<CompanyProjection> company = await session.Query.Query<CompanyProjection>().Where(x => x.CompanyId == accessLogged.CompanyId).ToListAsync();
            if (company.Count > 0)
            {
                CompanyProjection companyLogged = company.First();
                if (companyLogged.Status == Core.Enumerable.CompanyStatus.Inactive) return OperationResult<CompanyLoggedDto>.Invalid(new CompanyInactive());
                if (companyLogged.Status == Core.Enumerable.CompanyStatus.Suspended) return OperationResult<CompanyLoggedDto>.NotFound(new CompanyNotFound());
                if (companyLogged.Status == Core.Enumerable.CompanyStatus.Deleted) return OperationResult<CompanyLoggedDto>.NotFound(new CompanyNotFound());
                return OperationResult<CompanyLoggedDto>.Success(MapCompanyLogged.Map(companyLogged));
            }
            return OperationResult<CompanyLoggedDto>.NotFound(new CompanyNotFound());

        }
        return OperationResult<CompanyLoggedDto>.Unauthorize(new AccessNotFound());
    }
}