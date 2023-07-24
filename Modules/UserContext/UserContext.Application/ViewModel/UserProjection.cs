using UserContext.Core.Enumerable;

namespace UserContext.Application.ViewModel;

public record UserProjection
{
    public Guid Id { get; set; }
    public string Email { get; set; } = default!;
    public Status Status { get; set; }
    public string TimeZone { get; set; } = default!;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public string? Username { get; set; }
    public string? PhoneNumber { get; set; }
    public string? PhoneCountry { get; set; }
    public string? Picture { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Gender { get; set; }
    public DateTime Birth { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }
    public string? Street { get; set; }
    public string? StreetNumber { get; set; }
    public void Update() => UpdatedAt = DateTimeOffset.Now;
}