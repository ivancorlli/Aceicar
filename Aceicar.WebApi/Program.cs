
using Aceicar.WebApi.Extension;
using Aceicar.WebApi.Router;
using Oakton;

var _builder = WebApplication.CreateBuilder(args);

_builder.Host.ApplyOaktonExtensions();

IServiceCollection services = _builder.Services;

// Install Modules
services.InstallModules(_builder.Configuration, _builder.Environment);
// Install Wolverine
services.InstallWolverine(_builder.Host, _builder.Configuration);
// Auth Schema
services.AddAuthorization();

var app = _builder.Build();

app.UseAuthorization();
app.UseRouting();
app.ApiV1();


return await app.RunOaktonCommands(args);




