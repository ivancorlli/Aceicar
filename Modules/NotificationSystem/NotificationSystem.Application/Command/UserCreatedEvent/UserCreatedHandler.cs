
namespace NotificationSystem.Application.Command.UserCreatedEvent;

public static class UserCreatedHandler
{

    public static void Handle(object @event)
    {

        Console.WriteLine(@event);


    }


}