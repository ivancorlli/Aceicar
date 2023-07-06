using Aceicar.WebApi.Extension;
using Aceicar.WebApi.Router;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Oakton;

var _builder = WebApplication.CreateBuilder(args);

_builder.Host.ApplyOaktonExtensions();

IServiceCollection services = _builder.Services;


// Install Modules
services.InstallModules(_builder.Configuration, _builder.Environment);
// Install Wolverine
services.InstallWolverine(_builder.Host, _builder.Configuration);
// Auth Schema
services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.Authority = "https://dev-fzybni3fcogdesk7.us.auth0.com/";
            options.Audience = "weels";
        });
services.AddAuthorization();

var app = _builder.Build();

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.ApiV1();


return await app.RunOaktonCommands(args);




