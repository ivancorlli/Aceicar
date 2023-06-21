namespace Common.IntegrationEvents;

public record TwoFactorTokenGenerated(string UserId,string Code,string TokenType);