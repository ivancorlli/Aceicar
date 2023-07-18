namespace CompanyContext.Core.Event;

public sealed record ProductCreated(Guid ProductId, Guid CategoryId);