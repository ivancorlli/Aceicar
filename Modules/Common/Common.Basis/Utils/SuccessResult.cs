
using Common.Basis.Enum;
using Common.Basis.Interface;

namespace Common.Basis.Utils;

public class SuccessResult : OperationResult
{
    public SuccessResult() { }
    public override OperationResultType ResultType => OperationResultType.Ok;

    public override List<string> Errors => new List<string>();
}


public class SuccessResult<T> : OperationResult<T>
{
    private readonly T _data;
    public SuccessResult(T data)
    {
        _data = data;
    }
    public override OperationResultType ResultType => OperationResultType.Ok;

    public override List<string> Errors => new List<string>();

    public override T Data => _data;
}