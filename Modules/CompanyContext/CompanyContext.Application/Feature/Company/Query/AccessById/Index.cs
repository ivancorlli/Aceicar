using Common.Basis.Interface;
using Common.Basis.Utils;
using CompanyContext.Application.Feature.Company.Dto;
using CompanyContext.Application.Interface;
using CompanyContext.Application.ViewModel;
using CompanyContext.Core.Error;
using CompanyContext.Core.Repository;
using Marten;

namespace CompanyContext.Application.Feature.Company.Query.AccessById;

public sealed record AccessByIdQuery(Guid AccessId,Guid UserRequest);
public static class AccessByIdHandler
{
    public static async Task<IOperationResult<AccessByIdDto>> Handle(
        AccessByIdQuery query,
        IEventStoreQuery session,
        IRoleRepository _role,
        CancellationToken cancellationToken
    )
    {
        IReadOnlyList<UserAccess> list = await session.Query.Query<UserAccess>().Where(x => x.AccessId == query.AccessId).ToListAsync();
        if (list.Count > 0)
        {
            UserAccess access = list.First();
            if(access.UserId != query.UserRequest) return OperationResult<AccessByIdDto>.Unauthorize(new AccessIsDeleted());
            if (access.Status == Core.Enumerable.AccessStatus.Inactive) return OperationResult<AccessByIdDto>.Unauthorize(new AccessIsInactive());
            if (access.Status == Core.Enumerable.AccessStatus.Deleted) return OperationResult<AccessByIdDto>.Unauthorize(new AccessIsDeleted());
            CompanyContext.Core.Aggregate.Role? role = await _role.FindById(access.RoleId);
            AccessByIdDto response = MapAccessById.Map(access, role != null ? role.Name : string.Empty);
            return OperationResult<AccessByIdDto>.Success(response);
        }
        return OperationResult<AccessByIdDto>.NotFound(new AccessNotFound());
    }
}