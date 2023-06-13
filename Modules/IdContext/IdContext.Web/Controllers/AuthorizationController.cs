using System.Security.Claims;
using IdContext.Web.Constant;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;

namespace IdContext.Web.Controllers;

public class AuthorizationController : Controller
{
    [HttpGet("~/oauth/authorize")]
    [HttpPost("~/oauth/authorize")]
    [Authorize]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> Authorize()
    {
        var request = HttpContext.GetOpenIddictServerRequest() ??
                      throw new InvalidOperationException("Se produjo un error en el servidor Open ID.");

        // Obtenemos el usaurio de Identity
        var result = await HttpContext.AuthenticateAsync(IdentityConstants.ApplicationScheme);

        // Si no pudimos obtener el usuario, lo re enviamos al login
        if (!result.Succeeded)
        {
            return Challenge(
                authenticationSchemes: IdentityConstants.ApplicationScheme,
                properties: new AuthenticationProperties
                {
                    RedirectUri = Request.PathBase + Request.Path + QueryString.Create(
                        Request.HasFormContentType ? Request.Form.ToList() : Request.Query.ToList())
                });
        }

        // Obtenemos los claims
        var claims = new List<Claim>
        {
            //  Subject claim es el id del usuario
            new Claim(OpenIddictConstants.Claims.Subject, result.Principal.FindFirstValue(ClaimTypes.NameIdentifier)!)
            .SetDestinations(OpenIddictConstants.Destinations.IdentityToken),
            new Claim(OpenIddictConstants.Claims.Email, result.Principal.FindFirstValue(ClaimTypes.Email)!)
            .SetDestinations(OpenIddictConstants.Destinations.AccessToken),
            new Claim(OpenIddictConstants.Claims.Role, result.Principal.FindFirstValue(ClaimTypes.Role)!)
            .SetDestinations(OpenIddictConstants.Destinations.IdentityToken)
        };

        var claimsIdentity = new ClaimsIdentity(claims, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        //  Cargamos los scopes del cliente
        claimsPrincipal.SetScopes(request.GetScopes());

        // Iniciamos sesion en openiddict para seguir con el flujo authorization code
        return SignIn(claimsPrincipal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
    }

    [HttpPost("~/oauth/token")]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> Exchange()
    {
        var request = HttpContext.GetOpenIddictServerRequest() ??
        throw new InvalidOperationException("Se produjo un error en el servidor Open ID.");
        ClaimsPrincipal claimsPrincipal;
        if (request.IsClientCredentialsGrantType())
        {
            List<Claim> claims = new()
            {
                new Claim(OpenIddictConstants.Claims.Subject,request.ClientId!).SetDestinations(OpenIddictConstants.Destinations.AccessToken),
                new Claim(OpenIddictConstants.Claims.Audience,DefaultResource.WebApi.ToString()).SetDestinations(OpenIddictConstants.Destinations.AccessToken)
            };

            // Aniadimos los claims y el destino (osea el token al que se pegan)
            var identity = new ClaimsIdentity(claims, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            claimsPrincipal = new ClaimsPrincipal(identity);
            // Cargamos los scopes del cliente
            claimsPrincipal.SetScopes(request.GetScopes());
        }
        else if (request.IsAuthorizationCodeGrantType())
        {
            // Obtenemos todos los claims de las cookies
            AuthenticateResult result = await HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

            List<Claim> claims = new()
            {
                new Claim(OpenIddictConstants.Claims.Subject,result.Principal!.FindFirstValue(OpenIddictConstants.Claims.Subject)!)
					.SetDestinations(OpenIddictConstants.Destinations.AccessToken),
                new Claim(OpenIddictConstants.Claims.Audience,DefaultResource.WebApi.ToString())
					.SetDestinations(OpenIddictConstants.Destinations.AccessToken),
					new Claim(OpenIddictConstants.Claims.Role,result.Principal!.FindFirstValue(OpenIddictConstants.Claims.Role)!)
					.SetDestinations(OpenIddictConstants.Destinations.AccessToken)
            };

				// Aniadimos los claims y el destino (osea el token al que se pegan)
            var identity = new ClaimsIdentity(claims, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            claimsPrincipal = new ClaimsPrincipal(identity);
            // Cargamos los scopes del cliente
            claimsPrincipal.SetScopes(request.GetScopes());
        }

        else if (request.IsRefreshTokenGrantType())
        {
            // Obtenemos todos los claims del refresh token
            claimsPrincipal = (await HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme)).Principal!;
        }

        else
        {
            throw new InvalidOperationException("Grant Type no soportado.");
        }

        // iniciamos sesion y openiddict geestiona automaticamente los tokens
        return SignIn(claimsPrincipal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
    }

    [Authorize(AuthenticationSchemes = OpenIddictServerAspNetCoreDefaults.AuthenticationScheme)]
    [HttpGet("~/oauth/userinfo")]
    public async Task<IActionResult> Userinfo()
    {
        var claimsPrincipal = (await HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme)).Principal;

        return Ok(new
        {
            Name = claimsPrincipal!.GetClaim(OpenIddictConstants.Claims.Subject),
            Occupation = "Developer",
            Age = 43
        });
    }
}