using Common.Basis.Enum;
using Common.Basis.Interface;

namespace Common.Basis.Utils;

public sealed class OperationResult : IOperationResult
{
    public override OperationResultType ResultType { get; protected set; } = default!;
    public override List<string> Errors { get; protected set; } = default!;
    private OperationResult() { }
    public static OperationResult Success()
    {
        return new OperationResult() { ResultType = OperationResultType.Ok, Errors = new() };
    }

    public static OperationResult Invalid(string error)
    {
        OperationResult result = new();
        result.ResultType = OperationResultType.Invalid;
        result.Errors = new() { error ?? "The input was invalid." };
        return result;
    }

    public static OperationResult NotFound(string error)
    {
        OperationResult result = new();
        result.ResultType = OperationResultType.NotFound;
        result.Errors = new() { error ?? "The aggregate is not found." };
        return result;
    }
}

public class OperationResult<T> : IOperationResult<T>
{
    public override T Data { get; protected set; } = default(T)!;
    public override OperationResultType ResultType { get; protected set; } = default!;
    public override List<string> Errors { get; protected set; } = default!;
    private OperationResult() { }
    private OperationResult(T data)
    {
        Data = data;
    }

    public static OperationResult<T> Success(T data)
    {
        OperationResult<T> result = new(data);
        result.ResultType = OperationResultType.Ok;
        result.Errors = new();
        return result;
    }

    public static OperationResult<T> Invalid(string error)
    {
        OperationResult<T> result = new();
        result.ResultType = OperationResultType.Invalid;
        result.Errors = new() { error ?? "The input was invalid." };
        return result;
    }

        public static OperationResult<T> NotFound(string error)
    {
        OperationResult<T> result = new();
        result.ResultType = OperationResultType.NotFound;
        result.Errors = new() { error ?? "The aggregate is not found." };
        return result;
    }

}