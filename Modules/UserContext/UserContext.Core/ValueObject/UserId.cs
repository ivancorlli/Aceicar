using System;
namespace UserContext.Core.ValueObject;

public record UserId
{
    public Guid Value {get;init;}
    private UserId()
    {
        Value = Guid.NewGuid();
    }
    public static UserId Create()
    {
        UserId newUserId= new();
        return newUserId;
    }

}