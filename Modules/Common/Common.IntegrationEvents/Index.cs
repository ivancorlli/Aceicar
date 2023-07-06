namespace Common.IntegrationEvents;

public sealed record UserCreatedEvent(Guid UserId,string Email);