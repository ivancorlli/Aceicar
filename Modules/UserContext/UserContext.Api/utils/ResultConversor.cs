using Common.Basis.Enum;
using Common.Basis.Interface;

namespace UserContext.Api.utils;

public static class ResultConversor
{
    public static Microsoft.AspNetCore.Http.IResult Convert(IOperationResult result)
    {
        Microsoft.AspNetCore.Http.IResult response = Results.Empty;
        IError error = result.Errors.SingleOrDefault()!;
        switch (result.ResultType)
        {
            case OperationResultType.Ok:
                response = TypedResults.Ok();
                break;
            case OperationResultType.Invalid:
                response = TypedResults.BadRequest<ErrorResponse>(new(StatusCodes.Status400BadRequest, error.Type, error.Message));
                break;
            case OperationResultType.NotFound:
                response = TypedResults.NotFound<ErrorResponse>(new(StatusCodes.Status404NotFound, error.Type, error.Message));
                break;
            case OperationResultType.PartialOk:
                response = TypedResults.Conflict<ErrorResponse>(new(StatusCodes.Status409Conflict, error.Type, error.Message));
                break;
            case OperationResultType.PermissionDenied:
                response = TypedResults.Forbid();
                break;
            case OperationResultType.Unauthorized:
                response = TypedResults.Unauthorized();
                break;
            case OperationResultType.Unexpected:
                response = TypedResults.Problem(error.Message,error.Type,StatusCodes.Status500InternalServerError);
                break;
            default:
                response = TypedResults.Empty;
                break;
        }

        return response;
    }
        public static Microsoft.AspNetCore.Http.IResult Convert<T>(IOperationResult<T> result)
    {
        Microsoft.AspNetCore.Http.IResult response = Results.Empty;
        IError error = result.Errors.SingleOrDefault()!;
        switch (result.ResultType)
        {
            case OperationResultType.Ok:
                response = TypedResults.Ok(result.Data);
                break;
            case OperationResultType.Invalid:
                response = TypedResults.BadRequest<ErrorResponse>(new(StatusCodes.Status400BadRequest, error.Type, error.Message));
                break;
            case OperationResultType.NotFound:
                response = TypedResults.NotFound<ErrorResponse>(new(StatusCodes.Status404NotFound, error.Type, error.Message));
                break;
            case OperationResultType.PartialOk:
                response = TypedResults.Conflict<ErrorResponse>(new(StatusCodes.Status409Conflict, error.Type, error.Message));
                break;
            case OperationResultType.PermissionDenied:
                response = TypedResults.Forbid();
                break;
            case OperationResultType.Unauthorized:
                response = TypedResults.Unauthorized();
                break;
            case OperationResultType.Unexpected:
                response = TypedResults.Problem(error.Message,error.Type,StatusCodes.Status500InternalServerError);
                break;
            default:
                response = TypedResults.Empty;
                break;
        }

        return response;
    }
}