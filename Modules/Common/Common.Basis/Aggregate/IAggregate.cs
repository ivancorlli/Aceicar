using System.Runtime.Serialization;

namespace Common.Basis.Aggregate;

public abstract class IAggregate
{
    public Guid Id { get; protected set; }
    public long Version { get; protected set; }

    [IgnoreDataMember]
    private readonly List<object> _events = new List<object>();
    [IgnoreDataMember]
    public IEnumerable<object> Events => _events.AsReadOnly();
    public void Clear()
    {
        _events.Clear();
    }
    protected void Raise(object @event)
    {
        _events.Add(@event);
    }

}