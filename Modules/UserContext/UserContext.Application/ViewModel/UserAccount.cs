using UserContext.Core.Enumerable;
using UserContext.Core.ValueObject;

namespace UserContext.Application.ViewModel;

public record UserAccount
{
    public Guid Id { get; set; }
    public string Email { get; set; } = default!;
    public Status Status { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public string? Username { get; set; }
    public Phone? Phone { get; set; }
    public void Update()=> UpdatedAt=DateTimeOffset.Now;
}