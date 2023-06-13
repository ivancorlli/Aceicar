
var _builder = WebApplication.CreateBuilder(args);

IServiceCollection services = _builder.Services;
services.AddRazorPages();

// Install Modules
services.InstallModules(_builder.Configuration,_builder.Environment);
// Install key
services.InstallKey(_builder.Environment);
// Configure Cookies
services.InstallCookies(_builder.Configuration);
// Auth Schema
services.AddAuthorization();


var app = _builder.Build();

app.UseStaticFiles();
app.UseCookiePolicy();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();
app.MapRazorPages();
app.Run();



