
using Common.IntegrationEvents;
using IdContext.Core.Entity;
using Microsoft.AspNetCore.Identity;
using Wolverine;

namespace IdContext.Application.Command.GenerateTwoFactor;

public static class GenerateTwoFactorHandler
{
    public static async Task<string?> Handle(
        GenerateTwoFactorCommand command, 
        UserManager<User> _userManager,
        IMessageContext _bus
        
        )
    {

        string Code;
        string TokenType;
        User? user = await _userManager.FindByIdAsync(command.UserId);
        if (user is not null)
        {

            if (user.PhoneNumber is null || !user.PhoneNumberConfirmed)
            {
                TokenType = "Email";
                Code = await _userManager.GenerateTwoFactorTokenAsync(user, TokenType);
            }
            else
            {
                TokenType = "Phone";
                Code = await _userManager.GenerateTwoFactorTokenAsync(user, TokenType);
            }

            await _bus.PublishAsync(new TwoFactorTokenGenerated(user.Id,Code,TokenType));
            return TokenType;
        }else {
            return null;
        }
    }

}