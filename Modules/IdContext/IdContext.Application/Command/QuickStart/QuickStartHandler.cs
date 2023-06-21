using Common.IntegrationEvents;
using IdContext.Core.Entity;
using Microsoft.AspNetCore.Identity;
using Wolverine;

namespace IdContext.Application.Command.QuickStart;

public static class QuickStartHandler
{

    public static async void Handle(
        QuickStartCommand command,
        UserManager<User> _userManager,
        IMessageContext _bus
        
        )
    {

        User? user = await _userManager.FindByIdAsync(command.UserId);
        if(user is not null)
        {
            IdentityResult result = await _userManager.SetUserNameAsync(user,command.Username);
            result = await _userManager.SetPhoneNumberAsync(user,command.PhoneNumber);
            await _bus.PublishAsync(new UserPhoneUpdated(user.Id,command.PhoneNumber));
        }
    }
}