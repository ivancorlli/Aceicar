using Marten;
using UserContext.Core.Aggregate;
using UserContext.Core.Event;
using UserContext.Core.Repository;
using UserContext.Core.ValueObject;

namespace UserContext.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private IDocumentSession _session;

    public UserRepository(IDocumentSession session)
    {
        _session = session;
    }


    public void Create(User User)
    {
        _session.Events.StartStream<User>(User.Id,User.Events.ToArray());
        _session.SaveChanges();
    }

    public User? FindByEmail(Email Email)
    {
        var events = _session.Query<UserCreated>().ToList();
        var result =_session.Query<User>().Where(x=>x.Email.Value == Email.Value).ToList();
        if(result.Count > 0) return result.First();
            else return null;
    }

    public User? FindById(Guid Id)
    {
        throw new NotImplementedException();
    }

    public User? FindByPhone(Phone Phone)
    {
        throw new NotImplementedException();
    }

    public User? FindByUsername(Username Username)
    {
        throw new NotImplementedException();
    }

    public void Save(User Root)
    {
        throw new NotImplementedException();
    }
}