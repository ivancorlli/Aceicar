using UserContext.Core.Abstraction;
using UserContext.Core.ValueObject;

namespace UserContext.Core.Aggregate;

public class User : IUser
{
    internal User(Email email) : base(email)
    {
    }
}