using Common.Basis.Interface;
using CompanyContext.Api.utils;
using CompanyContext.Application.Feature.Company.Dto;
using CompanyContext.Application.Feature.Company.Query.CompanyLogged;
using Wolverine;

namespace CompanyContext.Api.Controller.Company;

public static class CompanyLogged
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Execute(
        Guid accessId,
        IMessageBus Bus,
        CancellationToken cancellationToken
    )   
    {
        IOperationResult<CompanyLoggedDto> result = await Bus.InvokeAsync<IOperationResult<CompanyLoggedDto>>(new CompanyLoggedQuery(accessId));
        if(result.ResultType == Common.Basis.Enum.OperationResultType.Ok) return TypedResults.Ok(result.Data);
        return ResultConversor.Convert(result);
    } 
}