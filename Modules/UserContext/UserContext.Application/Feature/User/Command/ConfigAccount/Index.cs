using Common.Basis.Interface;
using Common.Basis.Utils;
using Common.IntegrationEvents;
using UserContext.Core.Repository;
using UserContext.Core.Service;
using UserContext.Core.ValueObject;
using Common.Basis.ValueObject;

namespace UserContext.Application.Feature.User.Command.ConfigAccount;

public sealed record ConfigAccountCommand(
    Guid UserId,
    string Username,
    string PhoneCountry,
    string PhoneNumber
    );

public sealed class ConfigAccountHandler
{
    public static async Task<IOperationResult> Handle(ConfigAccountCommand command, IUoW _session, UserManager _manager, CancellationToken cancellationToken)
    {
        Username Username = Username.Create(command.Username);
        Phone Phone = Phone.Create(command.PhoneCountry, command.PhoneNumber);
        Result<UserContext.Core.Aggregate.User> result = await _manager.ConfigAccount(command.UserId,Username,Phone);
        if (result.IsFailure) return OperationResult.Invalid(result.Error);
        _session.UserRepository.Apply(result.Value);
        await _session.SaveChangesAsync(cancellationToken);
        Phone phone = result.Value.Phone!;
        await _session.UserRepository.Push(new UserPhoneChangedEvent(result.Value.Id,phone.Country,phone.Number));
        return OperationResult.Success();
    }
}