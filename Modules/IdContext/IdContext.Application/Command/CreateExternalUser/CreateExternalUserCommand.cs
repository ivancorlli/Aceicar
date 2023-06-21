
using Microsoft.AspNetCore.Identity;

namespace IdContext.Application.Command.CreateExternalUser;

public record CreateExternalUserCommand(string UserEmail,ExternalLoginInfo LoginInfo);