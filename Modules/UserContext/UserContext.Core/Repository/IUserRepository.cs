using Common.Basis.Repository;
using UserContext.Core.Aggregate;
using UserContext.Core.ValueObject;

namespace UserContext.Core.Repository;

public interface IUserRepository:IRepository<User,UserId>
{
   User? FindByEmail(Email Email); 
}