using Common.Basis.Interface;
using Common.Basis.Utils;
using NotificationSystem.Core.Aggregate;
using NotificationSystem.Core.Error;
using NotificationSystem.Core.Repository;

namespace NotificationSystem.Application.Feature.NotificationUser.Command.VerifyPhone;

public sealed record VerifyPhoneCommand(string UserId, string Code);
public static class VerifyPhoneHandler
{
    public static async Task<IOperationResult> Handle(
        VerifyPhoneCommand command,
        IUoW session,
        CancellationToken cancellationToken
    )
    {
        Guid userId = Guid.Parse(command.UserId);
        User? user = await session.UserRepository.FindById(userId);
        if (user == null) return OperationResult.NotFound(new UserNotFound());
        if (user.Phone == null) return OperationResult.Invalid(new PhoneNotFound());
        if (!user.Phone.Verified && user.Phone.VerificationCode != null)
        {
            bool verification = user.VerifyPhone(command.Code);
            if(!verification) return OperationResult.Invalid(new InvalidCode(command.Code));
        }
        return OperationResult.Success();
    }

}