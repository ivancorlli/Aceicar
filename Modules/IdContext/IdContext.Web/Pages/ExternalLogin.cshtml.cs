using IdContext.Core.Constant;
using IdContext.Core.Entity;
using IdContext.Core.Enumerable;
using IdContext.Web.Options;
using IdContext.Web.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace IdContext.Web.Pages;

[ValidateAntiForgeryToken]
public class ExternalLogin : PageModel
{
	private readonly SignInManager<User> _signInManager;
	private readonly UserManager<User> _userManager;
	private readonly RoleManager<Role> _roleManager;
	private readonly IUserStore<User> _userStore;
	private readonly IUserLoginStore<User> _userLogin;
	private readonly IOptions<RedirectUrlOptions> _redirectUrl;

	public string ReturnUrl { get; set; } = string.Empty;
	public string Error { get; set; } = string.Empty;
	public bool AllowBack { get; set; } = false;
	public ExternalLogin(
		SignInManager<User> signInManager,
		UserManager<User> userManager,
		RoleManager<Role> roleManager,
		IUserStore<User> userStore,
		IOptions<RedirectUrlOptions> returnUrl
		)
	{
		_signInManager = signInManager;
		_userManager = userManager;
		_userStore = userStore;
		_roleManager = roleManager;
		_redirectUrl = returnUrl;
		_userLogin = GetLogins();
	}

	public IActionResult OnGet() => RedirectToPage("/");

	public IActionResult OnPost(string provider, string returnUrl)
	{
		if (string.IsNullOrEmpty(returnUrl) || string.IsNullOrEmpty(provider))
			return Redirect("/");

		ReturnUrl = returnUrl;
		var redirectUrl = Url.Page("/ExternalLogin", pageHandler: "Callback", values: new { ReturnUrl });
		var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
		return new ChallengeResult(provider, properties);
	}

	public async Task<IActionResult> OnGetCallbackAsync(string? returnUrl = null, string? remoteError = null)
	{
		ReturnUrl = returnUrl ?? _redirectUrl.Value.Home.ToString();
		if (remoteError != null)
		{
			Error = $"Error del provedor: {remoteError}";
			return Page();
		}
		// Gets information about exteranl provider
		ExternalLoginInfo? info = await _signInManager.GetExternalLoginInfoAsync();
		if (info == null)
		{
			// If tehre is not information, return an error
			Error = "Se produjo un error al obtener tus datos.";
			return Page();
		}
		else
		{
			// Get Email claim
			string email = info.Principal.FindFirstValue(ClaimTypes.Email)!;
			// Search if user is already registered with this provider
			User? user = await _userLogin.FindByLoginAsync(info.LoginProvider, info.ProviderKey, CancellationToken.None);

			// IF user is not registered
			if (user == null)
			{
				// We search if the user has already an account with this email
				user = await _userManager.FindByEmailAsync(email);
				if (user is not null)
				{
					// If there is any account with this email, we add the login provider to this user

					// Add Login
					var result = await _userManager.AddLoginAsync(user, info);
					if (result.Succeeded)
					{
						// Check user status
						if (user.Status != UserStatus.Active)
						{
							Error = $"La cuenta {HideString.HideEmail(user.Email!)} no está activa. Comunicate con soporte";
							return Page();
						}
						else
						{
							var resul = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
							if (resul.Succeeded)
							{
								if (user.PhoneNumber is null ) return RedirectToPage($"/{_redirectUrl.Value.QuickStart}", new { ReturnUrl });
								else if (user.PhoneNumber is not null && !user.PhoneNumberConfirmed) return RedirectToPage($"/{_redirectUrl.Value.VerifyPhone}", new { ReturnUrl });
								else return new RedirectResult(ReturnUrl);

							}
							else if (resul.RequiresTwoFactor)
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
								return RedirectToPage($"/{_redirectUrl.Value.TwoFactor}", new { ReturnUrl, Remember = true, Token });
							}
							else if (resul.IsNotAllowed)
							{
								// If account is not verified
								Error = "Debes verificar tu cuenta, por favor revisa tu correo electronico";
								AllowBack = true;
								return Page();
							}
							else
							{
								Error = $"Se produjo un error al obtener tus datos de {info.ProviderDisplayName}.";
								AllowBack = true;
								return Page();
							}
						}
					}
					else
					{
						// if there is any error saving the provider
						foreach (var error in result.Errors)
						{
							Error = error.Description;
							break;
						}
						return Page();
					}



				}
				else
				// If the user dont have any account with this email, we create a new user
				{
					// Create a new external account
					user = IdContext.Core.Entity.User.CreateExternalUser(email);
					// save in database
					var result = await _userManager.CreateAsync(user);
					if (result.Succeeded)
					{
						// Aggreagate a rol, bydeafult an application role
						bool role = await _roleManager.RoleExistsAsync(DefaultRoles.ApplicationUser);
						if (role) await _userManager.AddToRoleAsync(user, DefaultRoles.ApplicationUser);
						else await _userManager.AddToRoleAsync(user, DefaultRoles.DefaultUser);


						// save the provider
						result = await _userManager.AddLoginAsync(user, info);
						if (result.Succeeded)
						{
							var Result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);

							if (Result.Succeeded)
							{
								await _messageSender.Send(Message.SendWelcome(
									user.Id,
									user.Email!
								));
								if (user.PhoneNumber is null ) return RedirectToPage($"/{_redirectUrl.Value.QuickStart}", new { ReturnUrl });
								else if (user.PhoneNumber is not null && !user.PhoneNumberConfirmed) return RedirectToPage($"/{_redirectUrl.Value.VerifyPhone}", new { ReturnUrl });
								else return new RedirectResult(ReturnUrl);
							}
							else
							{
								Error = $"Se produjo un error al obtener tus datos de {info.ProviderDisplayName}.";
								AllowBack = true;
								return Page();
							}
						}
						else
						{
							// if there is any error saving the provider
							foreach (var error in result.Errors)
							{
								Error = error.Description;
								break;
							}
							return Page();
						}
					}
					else
					{
						// Errors creating user
						foreach (var error in result.Errors)
						{
							Error = error.Description;
							break;
						}
						return Page();
					}
				}
			}
			else
			{
				// User already have and account
				if (user.Status != UserStatus.Active)
				{
					Error = $"La cuenta {HideString.HideEmail(user.Email!)} no está activa. Comunicate con soporte";
					return Page();
				}
				else
				{
					var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
					if (result.Succeeded)
					{
						if (user.PhoneNumber is null ) return RedirectToPage($"/{_redirectUrl.Value.QuickStart}", new { ReturnUrl });
						else if (user.PhoneNumber is not null && !user.PhoneNumberConfirmed) return RedirectToPage($"/{_redirectUrl.Value.VerifyPhone}", new { ReturnUrl });
						else return new RedirectResult(ReturnUrl);

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
						return RedirectToPage($"/{_redirectUrl.Value.TwoFactor}", new { ReturnUrl, Remember = true, Token });
					}
					else if (result.IsNotAllowed)
					{
						// If account is not verified
						Error = "Debes verificar tu cuenta, por favor revisa tu correo electronico";
						AllowBack = true;
						return Page();
					}
					else
					{
						Error = $"Se produjo un error al obtener tus datos de {info.ProviderDisplayName}.";
						AllowBack = true;
						return Page();
					}

				}
			}
		}
	}

	public IActionResult OnPostToSignIn(string url)
	{
		return RedirectToPage("/", new { ReturnUrl = url });
	}
	private IUserLoginStore<User> GetLogins()
	{
		return (IUserLoginStore<User>)_userStore;
	}
}