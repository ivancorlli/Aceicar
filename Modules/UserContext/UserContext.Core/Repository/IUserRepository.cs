using Common.Basis.Repository;
using UserContext.Core.Aggregate;
using UserContext.Core.Event.UserEvent;
using UserContext.Core.ValueObject;

namespace UserContext.Core.Repository;

public interface IUserRepository:IRepository<User>
{
   void Create(Guid UserId, UserCreated @event);
   Task<User?> FindByEmail(Email Email);
   Task<User?> FindByUsername(Username Username);
   Task<User?> FindByPhone(Phone Phone); 
}