
using Common.Basis.Enum;
using Common.Basis.Interface;

namespace Common.Basis.Utils;

public class InvalidResult: OperationResult
{
    private string _error;
    public InvalidResult(string error)
    {
        _error = error;
    }
    public override OperationResultType ResultType => OperationResultType.Invalid;

    public override List<string> Errors => new List<string> { _error ?? "The input was invalid." };
}


public class InvalidResult<T> : OperationResult<T>
{
    private string _error;
    public InvalidResult(string error)
    {
        _error = error;
    }
    public override OperationResultType ResultType => OperationResultType.Invalid;

    public override List<string> Errors => new List<string> { _error ?? "The input was invalid." };

    public override T Data => default(T)!;
}