namespace Common.IntegrationEvents;

public record UserCreated(string UserId,string UserEmail,bool IsExternalUser);