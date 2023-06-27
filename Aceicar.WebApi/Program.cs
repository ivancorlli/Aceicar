
using Aceicar.WebApi.Extension;
using Common.Basis.Interface;
using Oakton;
using UserContext.Application.Feature.ApplicationUser.Command.ConfigAccount;
using UserContext.Application.Feature.ApplicationUser.Command.CreateUser;
using UserContext.Core.Aggregate;
using Wolverine;

var _builder = WebApplication.CreateBuilder(args);

_builder.Host.ApplyOaktonExtensions();

IServiceCollection services = _builder.Services;


// Install Modules
services.InstallModules(_builder.Configuration, _builder.Environment);
// Auth Schema
services.AddAuthorization();
// Install Wolverine
services.InstallWolverine(_builder.Host, _builder.Configuration);

var app = _builder.Build();

app.UseAuthorization();

app.MapGet("/init", async (IMessageBus _bus) =>
{

    // OperationResult<User> result = await _bus.InvokeAsync<OperationResult<User>>(new CreateUserCommand("corlliivan@gmail.com"));
    OperationResult<User> result = await _bus.InvokeAsync<OperationResult<User>>(
    new ConfigAccountCommand("2f14e353-008b-4925-936f-e83b66abe852", "corlliivan", "AR", "3876410036")
      );

});


return await app.RunOaktonCommands(args);




