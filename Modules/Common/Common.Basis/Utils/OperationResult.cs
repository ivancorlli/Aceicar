using Common.Basis.Enum;
using Common.Basis.Interface;

namespace Common.Basis.Utils;

public sealed class OperationResult : IOperationResult
{    
    public override OperationResultType ResultType {get;protected set;} = default!;
    public override List<string> Errors {get;protected set;} = default!;
    private OperationResult(){}
    public static OperationResult Success()
    {
        return new OperationResult();
    }

    public static OperationResult Invalid(string error)
    {
        OperationResult result = new();
        result.Errors = new() {error ?? "The input was invalid."};
        return result;
    }
}

public class OperationResult<T> : IOperationResult<T>
{
    private readonly T _data = default(T)!;
    public override T Data {get;protected set;} = default(T)!;
    public override OperationResultType ResultType {get;protected set;} = default!;
    public override List<string> Errors {get;protected set;} = default!;
    private OperationResult(){}
    private OperationResult(T data)
    {
        _data=data;
    }

    public static OperationResult<T> Success(T data)
    {
        OperationResult<T> result = new(data);
        result.ResultType = OperationResultType.Ok;
        result.Errors = new();
        return  result;
    }

    public static OperationResult<T> Invalid(string error)
    {
        OperationResult<T> result = new();
        result.ResultType = OperationResultType.Invalid;
        result.Errors = new() {error ?? "The input was invalid." } ;
        return result;
    }

}