using Marten;
using UserContext.Application.ViewModel;
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

    public async Task<bool> IsEmailUsed(Email Email)
    {
        IEnumerable<UserAccount> result = await _query.Query<UserAccount>().Where(x => x.Email.ToLower() == Email.Value.ToLower()).ToListAsync();
        if (result.Count() > 0) return true;
        else return false;
    }

    public async Task<bool> IsPhoneUsed(Phone Phone)
    {
        IEnumerable<UserAccount> result = await _query.Query<UserAccount>().Where(
            x => x.Phone != null &&
            x.Phone.PhoneCountry.ToUpper() == Phone.PhoneCountry.ToUpper() &&
            x.Phone.PhoneNumber == Phone.PhoneNumber
            ).ToListAsync();
        if (result.Count() > 0) return true;
        else return false;
    }

    public async Task<bool> IsUsernameUsed(Username Username)
    {
        IEnumerable<UserAccount> result = await _query.Query<UserAccount>().Where(x => x.Username != null && x.Username.ToLower() == Username.Value.ToLower()).ToListAsync();
        if (result.Count() > 0) return true;
        else return false;
    }
}
