using Common.Basis.Interface;
using Common.Basis.Utils;
using UserContext.Core.Aggregate;
using UserContext.Core.Repository;
using UserContext.Core.Service;
using UserContext.Core.ValueObject;

namespace UserContext.Application.Feature.ApplicationUser.Command.ChangePhone;

public sealed record ChangePhoneCommand(string UserId,string PhoneCountry,string PhoneNumber);

public sealed class ChangePhoneHandler
{
    public static async Task<IOperationResult> Handle(
        ChangePhoneCommand command,
        IUoW _session,
        UserManager _manager,
        CancellationToken cancellationToken
        )
    {
        Guid UserId = Guid.Parse(command.UserId);
        Phone Phone = Phone.Create(command.PhoneCountry,command.PhoneNumber);
        Result<User> result = await _manager.ChangePhone(UserId,Phone);
        if(result.IsFailure) return OperationResult.Invalid(result.Error);
        _session.UserRepository.Apply(result.Value);
        return OperationResult.Success();
    }
}