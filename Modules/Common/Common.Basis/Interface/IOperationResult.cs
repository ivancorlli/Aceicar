using Common.Basis.Enum;

namespace Common.Basis.Interface;
public abstract class IOperationResult
{
    public abstract OperationResultType ResultType { get; protected set;}
    public abstract List<IError> Errors { get; protected set;}
}

public abstract class IOperationResult<T>
{
    public abstract OperationResultType ResultType { get; protected set;}
    public abstract List<IError> Errors { get; protected set;}
    public abstract T Data { get; protected set;}
}
