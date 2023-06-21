using System.Text;
using Common.IntegrationEvents;
using IdContext.Core.Entity;
using IdContext.Core.Enumerable;
using Microsoft.AspNetCore.Identity;
using Wolverine;

namespace IdContext.Application.Command.ForgotPassword;

public static class ForgotPasswordHandler
{

    public static async void Handle(
        ForgotPasswordCommand command,
        UserManager<User> _userManager,
        IUserLoginStore<User> _userLogin,
        CancellationToken token,
        IMessageBus _bus
        )
    {
        string Error;
        User? user = await _userManager.FindByEmailAsync(command.Email);
        if (user is null)
        {
            Error = "Usuario inexistente";
            return;
        }
        else if (!user.EmailConfirmed)
        {
            Error = "Tu cuenta no está verificada. Revisa tu correo electronico.";
            return;
        }
        else if (user.PasswordHash == null)
        {
            // Buscamos los logins externos del usuario
            var providers = await _userLogin.GetLoginsAsync(user, token);
            if (providers != null)
            {
                if (providers.Count > 0)
                {
                    var message = "";
                    if (providers.Count == 1)
                    {
                        message = providers[0].ProviderDisplayName!.ToString();
                    }
                    else
                    {

                        foreach (var provider in providers)
                        {
                            message += $"{provider.ProviderDisplayName}, ";
                        }
                    }
                    Error = $"No puedes acceder a este recurso. Puedes iniciar sesion con {message}.";
                    return;
                }
                else
                {
                    Error = "Usuario inexistente.";
                    return;
                }
            }
            else
            {
                Error = "Usuario inexistente.";
                return;
            }
        }
        else if (user.Status != UserStatus.Active)
        {
            Error = $"La cuenta {user.HideEmail} no está activa. Comunicate con soporte.";
            return;
        }
        else
        {

            string code = await _userManager.GeneratePasswordResetTokenAsync(user);
            await _bus.PublishAsync(new PasswordForgotten(user.Id, code, command.returnUrl));

        }

    }
}