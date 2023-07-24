using Common.Basis.Enum;
using Common.Basis.Interface;
using Microsoft.AspNetCore.Mvc;
using UserContext.Api.utils;
using UserContext.Application.Feature.ApplicationUser.Dto;
using UserContext.Application.Feature.User.Command.ChangePhone;
using UserContext.Application.Feature.User.Command.ChangePicture;
using UserContext.Application.Feature.User.Command.ChangeUsername;
using UserContext.Application.Feature.User.Command.ConfigAccount;
using UserContext.Application.Feature.User.Command.CreateUserWithProvider;
using UserContext.Application.Feature.User.Command.ProfileModified;
using Wolverine;

namespace UserContext.Api.Controller;

public sealed record CreateUserProviderRequest(
    string Email,
    string TimeZone,
    string? Picture,
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
        CreateUserWithProviderCommand command = new(Body.Email, Body.TimeZone);
        IOperationResult<CreateUserDto> result = await Bus.InvokeAsync<IOperationResult<CreateUserDto>>(command);
        if (result.ResultType == OperationResultType.Invalid) return TypedResults.BadRequest(new { Message = result.Errors.First() });

        if (!string.IsNullOrEmpty(Body.Username) && !string.IsNullOrEmpty(Body.PhoneCountry) && !string.IsNullOrEmpty(Body.PhoneNumber))
        {
            ConfigAccountCommand command1 = new(result.Data.UserId, Body.Username, Body.PhoneCountry, Body.PhoneNumber);
            await Bus.InvokeAsync(command1);
        }
        else
        {

            if (!string.IsNullOrEmpty(Body.Username))
            {
                ChangeUsernameCommand commnand2 = new(result.Data.UserId, Body.Username);
                await Bus.InvokeAsync(commnand2);
            }

            if (!string.IsNullOrEmpty(Body.PhoneCountry) && !string.IsNullOrEmpty(Body.PhoneNumber))
            {
                ChangePhoneCommand commnand2 = new(result.Data.UserId, Body.PhoneCountry, Body.PhoneNumber);
                await Bus.InvokeAsync(commnand2);
            }
        }

        if (!string.IsNullOrEmpty(Body.Name) && !string.IsNullOrEmpty(Body.Surname) && !string.IsNullOrEmpty(Body.Gender) && !string.IsNullOrEmpty(Body.Birth))
        {
            ModifyProfileCommand commnand3 = new(result.Data.UserId, Body.Name, Body.Surname, Body.Gender, DateTime.Parse(Body.Birth));
            await Bus.InvokeAsync(commnand3);
        }

        if (!string.IsNullOrEmpty(Body.Picture))
        {
            ChangePictureCommand command4 = new(result.Data.UserId, Body.Picture);
            await Bus.InvokeAsync(command4);
        }
        if (result.ResultType == OperationResultType.Created) return TypedResults.Created<CreateUserDto>($"/api/users/me?userId={result.Data.UserId}", result.Data);
        if (result.ResultType == OperationResultType.Ok) return TypedResults.Ok(result.Data);
        return ResultConversor.Convert(result);
    }
}