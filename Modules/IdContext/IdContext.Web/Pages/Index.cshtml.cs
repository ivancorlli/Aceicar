using System.Security.Claims;
using IdContext.Core.Entity;
using IdContext.Core.Enumerable;
using IdContext.Web.Options;
using IdContext.Web.Pages.Model;
using IdContext.Web.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace IdContext.Web.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IOptions<RedirectUrlOptions> _redirectUrl;
    private readonly SignInManager<User> _signIn;
    private readonly UserManager<User> _userManager;
    private readonly IUserStore<User> _userStore;
    private readonly IUserLoginStore<User> _userLogin;
    public string? ReturnUrl { get; set; } = string.Empty;
    public string Error { get; set; } = string.Empty;
    public List<AuthenticationScheme> ExternalLogins { get; set; } = new();
    [BindProperty]
    public SignInModel Login { get; set; } = new SignInModel();

    public IndexModel(
        ILogger<IndexModel> logger,
        IOptions<RedirectUrlOptions> defaultReturnUrl,
        UserManager<User> userManager,
        SignInManager<User> signIn,
        IUserStore<User> userStore
    )
    {
        _logger = logger;
        _redirectUrl = defaultReturnUrl;
        _signIn = signIn;
        _userManager = userManager;
        _userStore = userStore;
        _userLogin = GetLogins();
    }

    private IUserLoginStore<User> GetLogins()
    {
        return (IUserLoginStore<User>)_userStore;
    }

    public async Task<IActionResult> OnGetAsync(string? returnUrl = null)
    {

        // If user is not comming for a oauth client we attach a default callback route
        if (string.IsNullOrEmpty(returnUrl))
        {
            ReturnUrl = _redirectUrl.Value.Home.ToString();
        }
        else
        {
            ReturnUrl = returnUrl;
        }

        // Verify user is authenticated with the identity scheme
        var auth = await HttpContext.AuthenticateAsync(IdentityConstants.ApplicationScheme);
        if (auth.Succeeded)
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(id!.Value);
            if (user is not null)
            {
                if (user.Status != UserStatus.Active)
                {
                    // Delete all their cookies
                    await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
                    await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
                    Error = $"La cuenta {HideString.HideEmail(user.Email!)} no está activa. Comunicate con soporte";
                }
                else
                {
                    // If user exists, refresh his authentication cookie
                    await _signIn.RefreshSignInAsync(user);
                    // If phone number is not set, redirect to page
                    if (user.PhoneNumber is null) return RedirectToPage($"/{_redirectUrl.Value.QuickStart}", new { ReturnUrl });
                    else if (user.PhoneNumber is not null && !user.PhoneNumberConfirmed) return RedirectToPage($"/{_redirectUrl.Value.VerifyPhone}", new { ReturnUrl });
                    else return new RedirectResult(ReturnUrl);
                }
            }
            else
            {
                // Delete all their cookies
                await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
                await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            }
        }
        else
        {
            // Delete all their cookies
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
        }
        // Set all the external authentication methods
        var list = await _signIn.GetExternalAuthenticationSchemesAsync();
        ExternalLogins = list.ToList();
        return Page();
    }

    public async Task<IActionResult> OnPost(string? returnUrl = null)
    {
        if (string.IsNullOrEmpty(returnUrl))
        {
            ReturnUrl = _redirectUrl.Value.Home.ToString();
        }
        else
        {
            ReturnUrl = returnUrl;
        }
        // Sets all the external authentication methods
        var list = await _signIn.GetExternalAuthenticationSchemesAsync();
        ExternalLogins = list.ToList();
        // If the model is valid we can login the user
        if (!ModelState.IsValid)
        {
            return Page();
        }
        else
        {
            // search user
            User? user;
            if (Login.Email.Contains(char.Parse("@")))
            {
                // get user by the email
                var email = _userManager.NormalizeEmail(Login.Email);
                user = await _userManager.FindByEmailAsync(email);
            }
            else
            {
                // Get user by unsermae
                user = await _userManager.FindByNameAsync(Login.Email);
            }

            // If the user exists we con follow with login
            if (user != null)
            {
                // if user is not active dont let him login
                if (user.Status != UserStatus.Active)
                {
                    Error = $"La cuenta {HideString.HideEmail(user.Email!)} no está activa. Comunicate con soporte";
                    return Page();
                }
                // If the password is null, this meand the user has been authenticated with a social provider
                if (string.IsNullOrEmpty(user.PasswordHash))
                {

                    // search all the providers that the user has used to authenticate and return it in a informational message
                    var providers = await _userLogin.GetLoginsAsync(user, CancellationToken.None);
                    if (providers is not null)
                    {
                        if (providers.Count > 0)
                        {
                            var message = "";
                            if (providers.Count == 1)
                            {
                                message = providers[0].ProviderDisplayName!.ToString();
                            }
                            else
                            {
                                foreach (var provider in providers)
                                {
                                    message += $"{provider.ProviderDisplayName}, ";
                                }
                            }
                            Error = $"Puedes iniciar sesion con {message}";
                            return Page();
                        }
                        else
                        {
                            // If the user doesn't has any providers show an erro message
                            Error = "Usuario inexistente";
                            return Page();
                        }
                    }
                    else
                    {
                        // If the user doesn't has any providers show an erro message
                        Error = "Usuario inexistente";
                        return Page();
                    }
                }
                // Authenticate user with password
                var result = await _signIn.PasswordSignInAsync(user, Login.Password, Login.Remember, false);
                // If auth is succeed redirect user to corresponde page
                if (result.Succeeded)
                {
                    // If phone number is not set, redirect to page
                    if (user.PhoneNumber is null) return RedirectToPage($"/{_redirectUrl.Value.QuickStart}", new { ReturnUrl });
                    else if (user.PhoneNumber is not null && !user.PhoneNumberConfirmed) return RedirectToPage($"/{_redirectUrl.Value.VerifyPhone}", new { ReturnUrl });
                    else return new RedirectResult(ReturnUrl);
                }
                // If user is not allowed it's means user's email hasn't been verified.
                else if (result.IsNotAllowed)
                {
                    // Show an erro messsage 
                    Error = "Debes verificar tu cuenta, por favor revisa tu correo electronico";
                    return Page();
                }
                else if (result.RequiresTwoFactor)
                {
                    string Code;
                    string Token;
                    if (user.PhoneNumber is null || !user.PhoneNumberConfirmed)
                    {
                        Token = "Email";
                        Code = await _userManager.GenerateTwoFactorTokenAsync(user, Token);
                        await _messageSender.Send(Message.SendEmailTwoFactor(
                            user.Id,
                            user.Email!,
                            Code
                        ));
                    }
                    else
                    {
                        Token = "Phone";
                        Code = await _userManager.GenerateTwoFactorTokenAsync(user, Token);
                        await _messageSender.Send(Message.SendPhoneTwoFactor(
                            user.Id,
                            user.PhoneNumber,
                            Code
                        ));
                    }
                    return RedirectToPage($"/{_redirectUrl.Value.TwoFactor}", new { ReturnUrl, Login.Remember, Token });
                }
                else
                {
                    // If till there is an error, probably the password is wrong
                    Error = "Contraseña incorrecta";
                    return Page();
                }

            }
            else
            {
                // If the user doesn't exists show an erro message
                Error = "Usuario inexistente";
                return Page();
            }
        }

    }

}
