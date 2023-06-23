using System.Text.Json.Serialization;

namespace Common.Basis.Aggregate;

public abstract class IAggregate<T>
{
    public T Id { get; protected set; } = default!;

    // For protecting the state, i.e. conflict prevention
    // The setter is only public for setting up test conditions
    protected long Version { get; set; }

    // JsonIgnore - for making sure that it won't be stored in inline projection
    [JsonIgnore] private readonly List<object> _events = new List<object>();

    // Get the deltas, i.e. events that make up the state, not yet persisted
    public IEnumerable<object> Events => _events.AsReadOnly();

    // Mark the deltas as persisted.
    public void Clear()
    {
        _events.Clear();
    }

    protected void Raise(object @event)
    {
        // add the event to the uncommitted list
        _events.Add(@event);
    }
    
    protected void UpdateVersion()
    {
        Version++;
    }

}