using UserContext.Application.Utils;

namespace UserContext.Application.Feature.User.Dto;

public sealed record SuspendAccountDto
{
    public Guid UserId {get;set;}
    public string Status {get;set;} = default!;   
}


public static class MapSuspendAccountDto
{
    public static SuspendAccountDto Map(UserContext.Core.Aggregate.User user)
    {
        return new SuspendAccountDto()
        {
            UserId = user.Id,
            Status = StatusConversor.Convert(user.Status)
        };
    }
}