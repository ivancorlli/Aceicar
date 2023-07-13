using Common.Basis.Interface;
using Common.Basis.Utils;
using NotificationSystem.Core.Aggregate;
using NotificationSystem.Core.Error;
using NotificationSystem.Core.Repository;

namespace NotificationSystem.Application.Feature.NotificationUser.Command.VerifyEmail;

public sealed record VerifyEmailCommand(string UserId, string Code);
public static class VerifyEmailHandler
{
    public static async Task<IOperationResult> Handle(
        VerifyEmailCommand command,
        IUoW session,
        CancellationToken cancellationToken
    )
    {
        Guid UserId = Guid.Parse(command.UserId);
        User? user = await session.UserRepository.FindById(UserId);
        if (user == null) return OperationResult.NotFound(new UserNotFound());
        if (!user.Email.Verified && user.Email.VerificationCode != null)
        {
            bool verification = user.VerifyEmail(command.Code);
            if (!verification) return OperationResult.Invalid(new InvalidCode(command.Code));
        }
        return OperationResult.Success();
    }

}