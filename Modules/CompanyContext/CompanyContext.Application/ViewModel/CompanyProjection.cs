using CompanyContext.Core.Entity;
using CompanyContext.Core.Enumerable;

namespace CompanyContext.Application.ViewModel;

public sealed record Access();
public sealed record CompanyArea();
public class CompanyProjection
{
    public Guid CompanyId { get; set; }
    public Guid Owner { get; set; }
    public string Name { get; set; } = default!;
    public CompanyStatus Status { get; set; }
    public bool Published { get; set; } = default!;
    public DateTime PublishedAt { get; set; } = default!;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? PhoneCountry { get; set; }
    public string? Picture { get; set; }
    public string? Description { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }
    public string? Street { get; set; }
    public string? StreetNumber { get; set; }
    public string? Floor { get; set; } 
    public string? Apartment { get; set; } 
    public void Update() => UpdatedAt = DateTimeOffset.Now;
}