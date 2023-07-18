namespace CompanyContext.Api.utils;

public sealed record ErrorResponse(int Code,string Type,string Message);