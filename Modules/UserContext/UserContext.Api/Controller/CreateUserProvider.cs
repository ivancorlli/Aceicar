using Common.Basis.Enum;
using Common.Basis.Interface;
using Microsoft.AspNetCore.Mvc;
using UserContext.Application.Feature.ApplicationUser.Command.ChangePhone;
using UserContext.Application.Feature.ApplicationUser.Command.ChangeUsername;
using UserContext.Application.Feature.ApplicationUser.Command.ConfigAccount;
using UserContext.Application.Feature.ApplicationUser.Command.CreateUserWithProvider;
using UserContext.Application.Feature.ApplicationUser.Command.ProfileModified;
using UserContext.Core.ValueObject;
using Wolverine;

namespace UserContext.Api.Controller;

public sealed record CreateUserProviderRequest(
    string Email,
    string TimeZoneCountry,
    string TimeZone,
    string? Username,
    string? PhoneCountry,
    string? PhoneNumber,
    string? Name,
    string? Surname,
    string? Gender,
    string? Birth
    );
public static class CreateUserProvider
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Execute(
        [FromBody] CreateUserProviderRequest Body,
        IMessageBus Bus
    )
    {
        CreateUserWithProviderCommand command = new(Body.Email,Body.TimeZoneCountry,Body.TimeZone);
        IOperationResult<UserId> result = await Bus.InvokeAsync<IOperationResult<UserId>>(command);
        if(result.ResultType == OperationResultType.Invalid) return TypedResults.BadRequest(new {Message=result.Errors.First()});

        if(!string.IsNullOrEmpty(Body.Username) && !string.IsNullOrEmpty(Body.PhoneCountry)  && !string.IsNullOrEmpty(Body.PhoneNumber))
        {
            ConfigAccountCommand command1 = new(result.Data.Value.ToString(),Body.Username,Body.PhoneCountry,Body.PhoneNumber);
            IOperationResult result1 = await Bus.InvokeAsync<IOperationResult>(command1);
        }else {

            if(!string.IsNullOrEmpty(Body.Username))
            {
                ChangeUsernameCommand commnand2 = new(result.Data.Value.ToString(),Body.Username);
                IOperationResult result2 = await Bus.InvokeAsync<IOperationResult>(commnand2);
            }

            if(!string.IsNullOrEmpty(Body.PhoneCountry) && !string.IsNullOrEmpty(Body.PhoneNumber ))
            {
                ChangePhoneCommand commnand2 = new(result.Data.Value.ToString(),Body.PhoneCountry,Body.PhoneNumber);
                IOperationResult result2 = await Bus.InvokeAsync<IOperationResult>(commnand2);
            }
        }

        if( !string.IsNullOrEmpty(Body.Name) && !string.IsNullOrEmpty(Body.Surname) && !string.IsNullOrEmpty(Body.Gender) && !string.IsNullOrEmpty(Body.Birth))
        {
                ModifyProfileCommand commnand3 = new(result.Data.Value.ToString(),Body.Name,Body.Surname,Body.Gender,DateTime.Parse(Body.Birth));
                IOperationResult result3 = await Bus.InvokeAsync<IOperationResult>(commnand3);
        }


        return TypedResults.Ok(new {UserId=result.Data.Value});
    }
}