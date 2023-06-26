
using Aceicar.WebApi.Extension;
using Oakton;
using UserContext.Application.Feature.ApplicationUser.Command.CreateUser;
using Wolverine;

var _builder = WebApplication.CreateBuilder(args);

_builder.Host.ApplyOaktonExtensions();

IServiceCollection services = _builder.Services;


// Install Modules
services.InstallModules(_builder.Configuration,_builder.Environment);
// Auth Schema
services.AddAuthorization();
// Install Wolverine
services.InstallWolverine(_builder.Host,_builder.Configuration);

var app = _builder.Build();

app.UseAuthorization();

app.MapGet("/init",(IMessageBus _bus)=>{

    _bus.InvokeAsync(new CreateUserCommand("corlliivan@gmail.com"));

});


return await app.RunOaktonCommands(args);




