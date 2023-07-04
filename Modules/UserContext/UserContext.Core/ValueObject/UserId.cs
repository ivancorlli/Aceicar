using System;
namespace UserContext.Core.ValueObject;

public record UserId
{
    public Guid Value {get;init;}
    
    
    private UserId()
    {
        Value = Guid.NewGuid();
    }
    public UserId(string userId)
    {
        Value = Guid.Parse(userId);
    }
        public UserId(Guid userId)
    {
        Value = userId;
    }
    public static UserId Create()
    {
        UserId newUserId= new();
        return newUserId;
    }

}