using IdContext.Core.Constant;
using IdContext.Core.Entity;
using IdContext.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdContext.Infrastructure.Seed;

public class SeedRoles : IHostedService
{
	private readonly IServiceProvider _serviceProvider;

	public SeedRoles(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
	}
	public async Task StartAsync(CancellationToken cancellationToken)
	{
		using var scope = _serviceProvider.CreateScope();
		var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
		await context.Database.EnsureCreatedAsync(cancellationToken);
		var manager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

		if (!await manager.RoleExistsAsync(DefaultRoles.DefaultUser))
		{
			await manager.CreateAsync(new Role(DefaultRoles.DefaultUser, RolType.ApplicationRole));
		}



		if (!await manager.RoleExistsAsync(DefaultRoles.ApplicationUser))
		{
			await manager.CreateAsync(new Role(DefaultRoles.ApplicationUser, RolType.ApplicationRole));
		}

		if (!await manager.RoleExistsAsync(DefaultRoles.IdentityAdmin))
		{
			await manager.CreateAsync(new Role(DefaultRoles.IdentityAdmin, RolType.ProviderRole));
		}
	}

	public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}