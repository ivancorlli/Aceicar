using UserContext.Application.ViewModel;
using UserContext.Application.Utils;
namespace UserContext.Application.Feature.User.Dto;


public sealed record UserSummaryEmail(string Value, bool Verified = false);
public sealed record UserSummaryPhone(string Number, bool Verified = false);
public sealed record UserSummaryProfile(string Name, string Surname, string Gender, DateTime Birth);
public sealed record UserSummaryLocation(string Country, string City, string State, string PostalCode, string? Street, string? StreetNumber);
public sealed record UserSummary
{
    public Guid UserId { get; set; }
    public UserSummaryEmail Email { get; set; } = default!;
    public string Status { get; set; } = default!;
    public DateTimeOffset CreatedAt { get; set; }
    public string? Username { get; set; }
    public UserSummaryPhone? Phone { get; set; }
    public string? Picture { get; set; }
    public UserSummaryProfile? Profile { get; set; }
    public UserSummaryLocation? Location { get; set; }
}

public static class MapUserSummary
{
    public static UserSummary Map(UserProjection user)
    {
        return new UserSummary()
        {
            UserId = user.Id,
            Email = new UserSummaryEmail(user.Email),
            Status = StatusConversor.Convert(user.Status),
            CreatedAt = user.CreatedAt,
            Username = user.Username,
            Phone = user.PhoneNumber != null ? new UserSummaryPhone(user.PhoneNumber) : null,
            Picture = user.Picture,
            Profile = user.Name != null && user.Surname != null && user.Gender != null && user.Birth != null ? new UserSummaryProfile(user.Name, user.Surname, user.Gender, user.Birth) : null,
            Location = user.Country != null && user.City != null && user.State != null && user.PostalCode != null ? new UserSummaryLocation(user.Country, user.City, user.State, user.PostalCode, user.Street, user.StreetNumber) : null
        };
    }

}