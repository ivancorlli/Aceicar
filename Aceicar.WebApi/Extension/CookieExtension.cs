public static class CookieExtension
{

    public static IServiceCollection InstallCookies(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureApplicationCookie(opts =>
        {
            // paths
            opts.LoginPath = "/";
            opts.AccessDeniedPath = "";
            opts.ReturnUrlParameter = configuration["RedirectUrl:Home"]!.ToString();
            opts.SlidingExpiration = true;
            opts.ClaimsIssuer = "https://localhost:5000";
            // Cookie config
            opts.Cookie.Name = "_SIP";
            opts.Cookie.SameSite = SameSiteMode.Strict;
            opts.Cookie.HttpOnly = true;
            opts.Cookie.IsEssential = true;
            // Validate options
            opts.Validate();
        });

        services.ConfigureExternalCookie(x => x.Cookie.Name = "_EIP");

        services.Configure<CookiePolicyOptions>(options =>
        {
            options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
            options.OnAppendCookie = cookieContext =>
            {
                if (cookieContext.CookieOptions.SameSite == SameSiteMode.None)
                    cookieContext.CookieOptions.SameSite = SameSiteMode.Unspecified;
            };
            options.OnDeleteCookie = cookieContext =>
            {
                if (cookieContext.CookieOptions.SameSite == SameSiteMode.None)
                    cookieContext.CookieOptions.SameSite = SameSiteMode.Unspecified;
            };
        });
        
        services.AddAntiforgery(options =>
        {
            options.Cookie.Name = "_AIP";
        });


        return services;
    }

}