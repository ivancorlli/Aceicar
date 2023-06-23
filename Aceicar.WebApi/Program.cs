
using Aceicar.WebApi.Extension;
using Oakton;

var _builder = WebApplication.CreateBuilder(args);

IServiceCollection services = _builder.Services;
services.AddRazorPages();

// Install Modules
services.InstallModules(_builder.Configuration,_builder.Environment);
// Auth Schema
services.AddAuthorization();
// Install Wolverine
services.InstallWolverine(_builder.Host,_builder.Configuration);

var app = _builder.Build();

app.UseCookiePolicy();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();
app.MapRazorPages();
return await app.RunOaktonCommands(args);



