using UserContext.Application.ViewModel;

namespace UserContext.Application.Feature.ApplicationUser.Dto;

public sealed record CreateUserDto
{
    public Guid UserId { get; set; }
    public string Email { get; set; } = default!;
    public string? Username { get; set; } = default!;
    public string? Phone { get; set; }
    public string? Image { get; set; }
    public CreateUserProfile? Profile { get; set; }
    public CreateUserLocation? Location { get; set; }
}
public sealed record CreateUserProfile
{
    public string Name { get; set; } = default!;
    public string Surname { get; set; } = default!;
    public string Gender { get; set; } = default!;
    public string Birth { get; set; } = default!;
}

public sealed record CreateUserLocation
{
    public string Country { get; private set; } = default!;
    public string City { get; private set; } = default!;
    public string State { get; private set; } = default!;
    public string PostalCode { get; private set; } = default!;
    public string? Steet { get; private set; } = default!;
    public int? StreetNumber { get; private set; } = default!;
}


public static class MapCreateUserDto
{
    internal static CreateUserDto Map(UserContext.Core.Aggregate.User user)
    {
        return new CreateUserDto()
        {
            UserId = user.Id,
            Email = user.Email.Value,
        };
    }

    internal static CreateUserDto Map(UserProjection user)
    {
        return new CreateUserDto()
        {
            UserId = user.Id,
            Email = user.Email,
        };
    }
}