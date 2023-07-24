using Common.Basis.Interface;
using Common.Basis.Utils;
using Common.IntegrationEvents;
using UserContext.Core.Repository;
using UserContext.Core.Service;
using Common.Basis.ValueObject;

namespace UserContext.Application.Feature.User.Command.ChangePhone;

public sealed record ChangePhoneCommand(Guid UserId,string PhoneCountry,string PhoneNumber);

public sealed class ChangePhoneHandler
{
    public static async Task<IOperationResult> Handle(
        ChangePhoneCommand command,
        IUoW _session,
        UserManager _manager,
        CancellationToken cancellationToken
        )
    {
        Phone Phone = Phone.Create(command.PhoneCountry,command.PhoneNumber);
        Result<UserContext.Core.Aggregate.User> result = await _manager.ChangePhone(command.UserId,Phone);
        if(result.IsFailure) return OperationResult.Invalid(result.Error);
        _session.UserRepository.Apply(result.Value);
        await _session.SaveChangesAsync(cancellationToken);
        Phone phone = result.Value.Phone!;
        await _session.UserRepository.Push(new UserPhoneChangedEvent(result.Value.Id,phone.Country,phone.Number));
        return OperationResult.Success();
    }
}