namespace Common.Basis.Aggregate;

public abstract class IAggregate
{
    public Guid Id {get;protected set;}
    public long Version { get; protected set; }

}