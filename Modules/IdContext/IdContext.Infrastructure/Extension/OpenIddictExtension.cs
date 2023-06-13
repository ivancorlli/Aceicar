using System.Security.Cryptography.X509Certificates;
using IdContext.Infrastructure.Context;
using IdContext.Infrastructure.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace IdContext.Infrastructure.Extension;

internal static class OpenIddictExtension
{

    public static IServiceCollection InstallOpenid(this IServiceCollection services, IHostEnvironment env, IConfiguration configuration)
    {
        {
            
            // OpenId
            services.AddOpenIddict(x =>
            {
                // Config core
                x.AddCore(opts => opts.UseEntityFrameworkCore().UseDbContext<ApplicationDbContext>());


                x.AddServer(options =>
                {
                    options.AllowClientCredentialsFlow();
                    options.AllowAuthorizationCodeFlow().RequireProofKeyForCodeExchange();
                    options.AllowRefreshTokenFlow();

                    options
                        .SetAuthorizationEndpointUris("/oauth/authorize")
                        .SetTokenEndpointUris("/oauth/token");

                    // Encryption and signing of tokens
                    if (env.EnvironmentName != "Production")
                    {
                        options
                        .AddEncryptionKey(new SymmetricSecurityKey(Convert.FromBase64String("8BcrGnW9P9QXgX9ErWbl0+2182Sum5uCA5EtdtfILQE=")))
                        .AddSigningKey(new SymmetricSecurityKey(Convert.FromBase64String("BxM9xDh4xXvq4bywcu2VMtI82SdOpzd59nAyT9N0EOA=")));

                        options.DisableAccessTokenEncryption();
                    }
                    else
                    {
                        throw new NotImplementedException("Implementar Production Key for tokens");
                    }

                    // Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
                    options
                        .UseAspNetCore()
                        .EnableTokenEndpointPassthrough()
                        .EnableAuthorizationEndpointPassthrough();

                    // Certificates
                    if (env.EnvironmentName != "Production")
                    {
                        var encrypt = Path.Combine(env.ContentRootPath, "./Certificates/encryption-certificate-dev.pfx");
                        var signing = Path.Combine(env.ContentRootPath, "./Certificates/signing-certificate-dev.pfx");
                        X509Certificate2Collection e = new X509Certificate2Collection();
                        e.Import(encrypt, null, X509KeyStorageFlags.PersistKeySet);
                        X509Certificate2Collection s = new X509Certificate2Collection();
                        s.Import(signing, null, X509KeyStorageFlags.PersistKeySet);
                        options.AddEncryptionCertificate(e.First())
                            .AddSigningCertificate(s.First());
                    }
                    else
                    {
                        var encrypt = Path.Combine(env.ContentRootPath, "Certificates\\encryption-certificate.pfx");
                        var signing = Path.Combine(env.ContentRootPath, "Certificates\\signing-certificate.pfx");
                        X509Certificate2Collection e = new X509Certificate2Collection();
                        e.Import(encrypt, null, X509KeyStorageFlags.PersistKeySet);
                        X509Certificate2Collection s = new X509Certificate2Collection();
                        s.Import(signing, null, X509KeyStorageFlags.PersistKeySet);
                        options.AddEncryptionCertificate(e.First())
                            .AddSigningCertificate(s.First());
                    }

                    // Issuer
                    options.SetIssuer(new Uri("https://localhost:5005"));
                });

            });


            return services;
        }

    }
}
