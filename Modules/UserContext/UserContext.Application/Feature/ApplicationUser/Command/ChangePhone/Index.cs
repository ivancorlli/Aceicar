using Common.Basis.Interface;
using Common.Basis.Utils;
using Common.IntegrationEvents;
using UserContext.Core.Aggregate;
using UserContext.Core.Repository;
using UserContext.Core.Service;
using Common.Basis.ValueObject;

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
        await _session.SaveChangesAsync(cancellationToken);
        Phone phone = result.Value.Phone!;
        await _session.UserRepository.Push(new UserPhoneChangedEvent(result.Value.Id,phone.Country,phone.Number));
        return OperationResult.Success();
    }
}