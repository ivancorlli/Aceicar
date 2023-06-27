using Marten;
using UserContext.Core.Aggregate;
using UserContext.Core.Event.UserEvent;
using UserContext.Core.Repository;
using UserContext.Core.ValueObject;

namespace UserContext.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private IDocumentSession _session;
    private IQuerySession _query;

    public UserRepository(
        IDocumentSession session,
        IQuerySession query

        )
    {
        _session = session;
        _query = query;
    }

    public void Apply(Guid Id, params object[] @events)
    {
        if (events.ToList().Count > 0)
        {
            foreach (var item in events.ToList())
            {
                _session.Events.Append(Id, item);
            }

        }
    }

    public void Create(Guid UserId, UserCreated @event)
    {
        _session.Events.StartStream<User>(UserId, @event);
    }

    public async Task<User?> FindByEmail(Email Email)
    {
        var result = await _query.Query<User>().Where(x => x.Email.Value == Email.Value).ToListAsync();
        if (result.Count > 0) return result.First();
        else return null;
    }

    public async Task<User?> FindById(Guid Id)
    {
        var result = await _query.Events.AggregateStreamAsync<User>(Id);
        if (result != null) return result;
        else return null;
    }

    public async Task<User?> FindByPhone(Phone Phone)
    {
        var result = await _query.Query<User>().Where(x => x.Phone != null && x.Phone.PhoneNumber == Phone.PhoneNumber).ToListAsync();
        if (result.Count > 0) return result.First();
        else return null;
    }

    public async Task<User?> FindByUsername(Username Username)
    {
        var result = await _query.Query<User>().Where(x => x.Username != null && x.Username.Value == Username.Value).ToListAsync();
        if (result.Count > 0) return result.First();
        else return null;
    }
}