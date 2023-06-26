using Common.Basis.Repository;
using UserContext.Core.Aggregate;
using UserContext.Core.ValueObject;

namespace UserContext.Core.Repository;

public interface IUserRepository:IRepository<User>
{
   void Create(User User);
   User? FindByEmail(Email Email);
   User? FindByUsername(Username Username);
   User? FindByPhone(Phone Phone); 
}