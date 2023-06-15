using IdContext.Application.Enumerable;

namespace IdContext.Application.Command.Generate2FactorToken;

public record Generate2FactorTokenCommand(string UserId, string UserEmail,string Code,TwoFactorToken Token);