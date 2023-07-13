using Common.IntegrationEvents;
using NotificationSystem.Core.Aggregate;
using NotificationSystem.Core.Repository;
using NotificationSystem.Core.ValueObject;

namespace NotificationSystem.Application.EventHandler;

public static class PhoneChangedHandler
{

    public static async void Handle(
        UserPhoneChangedEvent command,
        IUoW session,
        CancellationToken cancellationToken
    )
    {
        User? user = await session.UserRepository.FindById(command.UserId);
        if(user!=null)
        {
            Phone phone = Phone.Create(command.Country,command.Number);
            string code = phone.GenerateVerificationCode();
            user.ChangePhone(phone,code);
            session.UserRepository.Apply(user);
            await session.SaveChangesAsync(cancellationToken);
        }
    }

}   