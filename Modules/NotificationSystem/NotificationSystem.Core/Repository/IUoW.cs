using Common.Basis.Repository;

namespace NotificationSystem.Core.Repository;

public interface IUoW:IUnitOfWork
{
    public IUserRepository UserRepository {get;} 
}