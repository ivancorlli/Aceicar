using Common.Basis.Interface;
using Common.Basis.Utils;
using UserContext.Core.Aggregate;
using UserContext.Core.Event.UserEvent;
using UserContext.Core.Repository;
using UserContext.Core.Service;
using UserContext.Core.ValueObject;

namespace UserContext.Application.Feature.ApplicationUser.Command.ConfigAccount;

public record ConfigAccountCommand(
    string UserId,
    string Username,
    string PhoneCountry,
    string PhoneNumber
    );

public class ConfigAccountHandler
{
    public async Task<OperationResult> Handle(ConfigAccountCommand command, IUoW _session, UserManager _manager, CancellationToken cancellationToken)
    {
        UserId UserId = new(command.UserId);
        Username Username = Username.Create(command.Username);
        Phone Phone = Phone.Create(command.PhoneCountry, command.PhoneNumber);
        Result<UsernameChanged> username = await _manager.ChangeUsername(UserId,Username);
        if (username.IsFailure) return new InvalidResult(username.Error.Message);
        Result<PhoneChanged> phone = await _manager.ChangePhone(UserId,Phone);
        if(phone.IsFailure) return new InvalidResult(phone.Error.Message);
        PhoneChanged phoneChanged = phone.Value;
        UsernameChanged usernameChanged = username.Value;
        _session.UserRepository.Apply(UserId.Value,phoneChanged,usernameChanged);
        await _session.SaveChangesAsync(cancellationToken);
        return new SuccessResult();
    }
}