namespace IdContext.Application.Command.ForgotPassword;

public record ForgotPasswordCommand(string Email,string returnUrl);
