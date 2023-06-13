using Microsoft.AspNetCore.Identity;

namespace IdContext.Core.Entity;

public class Role : IdentityRole
{
	public string Type { get; private set; } = default!;

	private Role() { }
	public Role(string name, string type)
	{
		Name = name.Trim();
		Type = type;
	}
}