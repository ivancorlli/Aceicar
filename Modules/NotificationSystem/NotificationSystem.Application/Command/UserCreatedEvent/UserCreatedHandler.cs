using Common.IntegrationEvents;

namespace NotificationSystem.Application.Command.UserCreatedEvent;

public static class UserCreatedHandler
{

    public static void Handle(UserCreated @event)
    {

        Console.WriteLine(@event);


    }


}