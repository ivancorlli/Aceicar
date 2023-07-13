using Common.IntegrationEvents;
using NotificationSystem.Core.Aggregate;
using NotificationSystem.Core.Repository;
using NotificationSystem.Core.ValueObject;

namespace NotificationSystem.Application.EventHandler;

public static class EmailChangedEventHandler
{
    public static async void Handle(
        UserEmailChangedEvent command,
        IUoW session,
        CancellationToken cancellationToken
    )
    {
        User? user = await session.UserRepository.FindById(command.UserId);
        if(user != null)
        {
            Email email = Email.Create(command.Email);
            string code = email.GenerateVerificationCode();
            user.ChangeEmail(email,code);
            session.UserRepository.Apply(user);
            await session.SaveChangesAsync(cancellationToken);
        }
    }
}