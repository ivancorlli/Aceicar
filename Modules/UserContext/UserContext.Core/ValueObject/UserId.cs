using System;
namespace UserContext.Core.ValueObject;

public record UserId
{
    public Guid Value {get;protected set;}
    
    public static UserId Parse(string userId)
    {
        UserId newUserId= new(); 
        newUserId.Value = Guid.Parse(userId);
        return newUserId;
    }
    public static UserId Parse(Guid userId)
    {
        UserId newUserId= new(); 
        newUserId.Value = userId;
        return newUserId;
    }
    public static UserId Create()
    {
        UserId newUserId= new();
        newUserId.Value = Guid.NewGuid();
        return newUserId;
    }

}