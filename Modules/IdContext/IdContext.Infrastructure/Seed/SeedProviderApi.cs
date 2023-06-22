using IdContext.Infrastructure.Constant;
using IdContext.Infrastructure.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using OpenIddict.Abstractions;

namespace IdContext.Infrastructure.Seed;


public class SeedProviderApi : IHostedService
{
	private readonly IServiceProvider _serviceProvider;
	private readonly IOptions<ProviderClientOptions> _providerClient;

	public SeedProviderApi(IServiceProvider serviceProvider,IOptions<ProviderClientOptions> providerClient)
	{
		_serviceProvider = serviceProvider;
		_providerClient = providerClient;
	}

	public async Task StartAsync(CancellationToken cancellationToken)
	{
		using var scope = _serviceProvider.CreateScope();

		var _scope = scope.ServiceProvider.GetRequiredService<IOpenIddictScopeManager>();
		var _client = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

		foreach(string item in ApiScopesList.Scopes)
		{
			if (await _scope.FindByNameAsync(item, cancellationToken) is null)
			{
				await _scope.CreateAsync(new OpenIddictScopeDescriptor
				{
					
					Name=item,
					DisplayName=item,

				}, cancellationToken);
			}
		}

		if(_providerClient.Value is not null)
		{
			if(await _client.FindByClientIdAsync(_providerClient.Value.ClientId) is null)
			{
				OpenIddictApplicationDescriptor newClient = new() {
						DisplayName= _providerClient.Value.Name,
						ClientId = _providerClient.Value.ClientId.Trim(),
						ClientSecret = _providerClient.Value.ClientSecret.Trim(),
						RedirectUris = {new Uri(_providerClient.Value.LoginUrl.Trim())},
						PostLogoutRedirectUris = {new Uri(_providerClient.Value.LogoutUrl)},
						Permissions = {
							OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
							OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
							OpenIddictConstants.Permissions.GrantTypes.ClientCredentials,
							OpenIddictConstants.Permissions.Endpoints.Authorization,
							OpenIddictConstants.Permissions.Endpoints.Token,
							OpenIddictConstants.Permissions.Endpoints.Logout,
							OpenIddictConstants.Permissions.ResponseTypes.Code,
							OpenIddictConstants.Permissions.ResponseTypes.IdToken,
							OpenIddictConstants.Permissions.ResponseTypes.Token,
							OpenIddictConstants.Permissions.Scopes.Profile,
							OpenIddictConstants.Permissions.Scopes.Email,
							OpenIddictConstants.Permissions.Scopes.Phone,
							OpenIddictConstants.Permissions.Scopes.Roles,
						},
						Requirements = {
							OpenIddictConstants.Requirements.Features.ProofKeyForCodeExchange,
						} 
				};

				foreach(string item in ApiScopesList.Scopes)
				{
					newClient.Permissions.Add(OpenIddictConstants.Permissions.Prefixes.Scope + item);
				}

				await _client.CreateAsync(newClient);

			}
		}


	}

	public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
