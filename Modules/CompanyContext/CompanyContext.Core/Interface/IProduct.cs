using CompanyContext.Core.Event;

namespace CompanyContext.Core.Interface;

public abstract class IProduct
{
    public Guid Id { get; protected set; } = default!;
    public string Code { get; protected set; } = default!;
    public IList<string> Images { get;private set; } = new List<string>();
    public void Apply(ImageAdded @event)
    {
        Images.Add(@event.Image);
    }
    public void Apply(ImageDeleted @event)
    {
        IList<string> exists = Images.Where(x=>x == @event.Image).ToList();
        if(exists.Count > 0)
        {
            Images.Remove(exists.First());
        }
    }
}