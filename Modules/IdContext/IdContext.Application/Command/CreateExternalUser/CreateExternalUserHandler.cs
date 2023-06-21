using Common.IntegrationEvents;
using IdContext.Core.Constant;
using IdContext.Core.Entity;
using Microsoft.AspNetCore.Identity;
using Wolverine;

namespace IdContext.Application.Command.CreateExternalUser;

public static class CreateExternalUserHandler
{

    public static async Task<User?> Handle(
        CreateExternalUserCommand command, 
        UserManager<User> _userManager, 
        RoleManager<Role> _roleManager,
        IMessageBus _bus
        )
    {
        // Create a new external account
        User user = User.CreateExternalUser(command.UserEmail);
        // save in database
        IdentityResult result = await _userManager.CreateAsync(user);
        if (result.Succeeded)
        {
            // Aggreagate a rol, bydeafult an application role
            bool role = await _roleManager.RoleExistsAsync(DefaultRoles.ApplicationUser);
            if (role) await _userManager.AddToRoleAsync(user, DefaultRoles.ApplicationUser);
            else await _userManager.AddToRoleAsync(user, DefaultRoles.DefaultUser);
            
            // save the provider
            result = await _userManager.AddLoginAsync(user, command.LoginInfo);
            if (result.Succeeded)
            {
                // Send Welcome Email
                await _bus.PublishAsync(new UserCreated(user.Id,user.Email!,user.IsAuthenticatedExternaly));
                return user;
            }
            else
            {
                // if there is any error saving the provider
               return null;
            }
        }
        else
        {
            return null;

        }
    }
}