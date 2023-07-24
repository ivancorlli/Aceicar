using Common.Basis.Interface;
using Common.Basis.Utils;
using Marten;
using UserContext.Application.Feature.User.Dto;
using UserContext.Application.Interface;
using UserContext.Application.ViewModel;
using UserContext.Core.Error;

namespace UserContext.Application.Feature.ApplicationUser.Query.MyData;


public sealed record MyDataQuery(Guid UserId);
public sealed class MyDataHandler
{

    public static async Task<IOperationResult<UserSummary>> Handle(
        MyDataQuery query,
        IApplicationQuery session,
        CancellationToken token
    )
    {
        IReadOnlyList<UserProjection> user =  await session.Query.Query<UserProjection>().Where(x=>x.Id == query.UserId).ToListAsync(token);
        if(user.Count <= 0) return OperationResult<UserSummary>.NotFound(new UserNotFound());
        return OperationResult<UserSummary>.Success(MapUserSummary.Map(user.First()));
    }

}