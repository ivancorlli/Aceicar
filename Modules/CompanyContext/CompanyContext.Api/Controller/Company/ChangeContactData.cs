using Common.Basis.Interface;
using CompanyContext.Api.utils;
using CompanyContext.Application.Feature.Company.Command.ChangeContactData;
using Wolverine;

namespace CompanyContext.Api.Controller.Company;

public sealed record ChangeContactDataBody(
    string email,
    string country,
    string number
);
public static class ChangeContactData
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Execute(
        Guid companyId,
        ChangeContactDataBody body,
        IMessageBus bus,
        CancellationToken cancellationToken
    )
    {
        ChangeContactDataCommand command = new(companyId,body.email,body.country,body.number);
        IOperationResult result = await bus.InvokeAsync<IOperationResult>(command,cancellationToken);
        return ResultConversor.Convert(result);
    }
}