using Common.Basis.Repository;

namespace UserContext.Core.Repository;

public interface IUoW:IUnitOfWork
{
    public IUserRepository UserRepository {get;} 
}