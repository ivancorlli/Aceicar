using Marten;
using NotificationSystem.Core.Aggregate;
using NotificationSystem.Core.Event.UserEvent;
using NotificationSystem.Core.Repository;

namespace NotificationSystem.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private IDocumentSession _session;
    public UserRepository(IDocumentSession session)
    {
        _session = session;
    }
    public void Apply(User Root)
    {
        Guid Id = Root.Id;
        IEnumerable<object> events = Root.Events;
        if (events.ToList().Count > 0)
        {
            foreach (var item in events.ToList())
            {
                _session.Events.Append(Id, item);
            }

        }
        Root.Clear();
    }

    public void Create(Guid UserId, UserCreated @event)
    {
        _session.Events.StartStream<User>(UserId, @event);
    }

    public async Task<User?> FindById(Guid Id)
    {
         var result = await _session.Events.FetchForWriting<User>(Id);
        if (result != null) return result.Aggregate;
        else return null;
    }

    public void Push<T>(T IntegrationEvent)
    {
        throw new NotImplementedException();
    }
}