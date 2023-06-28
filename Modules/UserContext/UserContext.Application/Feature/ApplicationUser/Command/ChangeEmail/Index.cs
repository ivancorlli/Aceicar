using Common.Basis.Interface;
using Common.Basis.Utils;
using UserContext.Core.Aggregate;
using UserContext.Core.Repository;
using UserContext.Core.Service;
using UserContext.Core.ValueObject;

namespace UserContext.Application.Feature.ApplicationUser.Command.ChangeEmail;

public sealed record ChangeEmailCommand(string UserId,string Email);

public sealed class ChangeEmailHandler
{
    public static async Task<OperationResult> Handle(
        ChangeEmailCommand command,
        IUoW _session,
        UserManager _manager,
        CancellationToken cancellationToken
    ) 
    {
        UserId UserId = new(command.UserId);
        Email Email = Email.Create(command.Email);
        Result<User> result = await _manager.ChangeEmail(UserId,Email);
        if(result.IsFailure) return new InvalidResult(result.Error.Message);
        _session.UserRepository.Apply(result.Value);
        await _session.SaveChangesAsync(cancellationToken);
        return new SuccessResult();
    }  
}