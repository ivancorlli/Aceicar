using Common.Basis.Interface;
using Common.Basis.Utils;
using UserContext.Application.Service;
using UserContext.Application.ViewModel;
using UserContext.Core.Error;
using UserContext.Core.ValueObject;

namespace UserContext.Application.Feature.ApplicationUser.Query.MyData;


public sealed record MyDataCommand(string UserId);
public sealed class MyDataHandler
{

    public static async Task<IOperationResult<UserAccount>> Handle(
        MyDataCommand command,
        IUserAccountService service,
        CancellationToken token
    )
    {
        Guid userId = Guid.Parse(command.UserId);
        UserAccount? user = await service.FindById(userId);
        if(user == null) return OperationResult<UserAccount>.NotFound(new UserNotFound());
        return OperationResult<UserAccount>.Success(user);
    }

}