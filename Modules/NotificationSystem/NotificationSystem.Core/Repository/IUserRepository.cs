using Common.Basis.Repository;
using NotificationSystem.Core.Aggregate;
using NotificationSystem.Core.Event.UserEvent;

namespace NotificationSystem.Core.Repository;

public interface IUserRepository:IRepository<User>
{
   void Create(Guid UserId, UserCreated @event);
}