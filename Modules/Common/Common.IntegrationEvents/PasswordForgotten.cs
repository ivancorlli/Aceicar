namespace Common.IntegrationEvents;

public record PasswordForgotten(string UserId,string Code,string ReturnUrl);