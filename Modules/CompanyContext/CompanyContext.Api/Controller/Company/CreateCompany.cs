using Common.Basis.Interface;
using CompanyContext.Api.utils;
using CompanyContext.Application.Feature.Company.Command.CreateCompany;
using Wolverine;

namespace CompanyContext.Api.Controller.Company;

public sealed record CreateCompanyBody(Guid TypeId, Guid SpecializationId, Guid Owner, string Name);
public static class CreateCompany
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Execute(
        CreateCompanyBody Body,
        IMessageBus Bus,
        CancellationToken cancellationToken
    )
    {
        CreateCompanyCommand command = new(Body.TypeId, Body.SpecializationId, Body.Owner, Body.Name);
        IOperationResult result = await Bus.InvokeAsync<IOperationResult>(command);
        return ResultConversor.Convert(result);
    }

}