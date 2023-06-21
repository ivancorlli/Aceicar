
using IdContext.Core.Entity;

namespace IdContext.Application.Command.GenerateTwoFactor;

public record GenerateTwoFactorCommand(string UserId);