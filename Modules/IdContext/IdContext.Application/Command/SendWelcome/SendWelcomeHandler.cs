using IdContext.Application.Command.SendWelcome;

public class SendWelcomeHandler
{
   public void Handle(SendWelcomeCommand command)
   {
        Console.WriteLine($"Welcome to Aceicar {command.UserEmail}");
   } 
}