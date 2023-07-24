using Common.Basis.Interface;
using CompanyContext.Api.utils;
using CompanyContext.Application.Feature.Company.Command.ChangeLocation;
using Wolverine;

namespace CompanyContext.Api.Controller.Company;

public sealed record ChangeLocationBody(
    string country,
    string city,
    string state,
    string postalCode,
    string street,
    string streetNumber,
    string? floor,
    string? apartment
);
public static class ChangeLocation
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Execute(
        Guid companyId,
        ChangeLocationBody body,
        IMessageBus bus,
        CancellationToken cancellationToken
    )
    {
        ChangeLocationCommand command = new(
            companyId,
            body.country,
            body.city,
            body.state,
            body.postalCode,
            body.street,
            body.streetNumber,
            body.floor,
            body.apartment
        );
        IOperationResult result = await bus.InvokeAsync<IOperationResult>(command,cancellationToken);
        return ResultConversor.Convert(result);
    }
}