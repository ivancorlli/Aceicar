using Common.Basis.Enum;

namespace Common.Basis.Interface;
public abstract class OperationResult
{
    public abstract OperationResultType ResultType { get; }
    public abstract List<string> Errors { get; }
}

public abstract class OperationResult<T>
{
    public abstract OperationResultType ResultType { get; }
    public abstract List<string> Errors { get; }
    public abstract T Data { get; }
}
