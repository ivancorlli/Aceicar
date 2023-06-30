using Common.Basis.Repository;
using UserContext.Core.Aggregate;
using UserContext.Core.Event.UserEvent;
using UserContext.Core.ValueObject;

namespace UserContext.Core.Repository;

public interface IUserRepository:IRepository<User>
{
   void Create(Guid UserId, UserCreated @event);
   Task<bool> IsEmailUsed(Email Email);
   Task<bool> IsUsernameUsed(Username Username);
   Task<bool> IsPhoneUsed(Phone Phone); 
}