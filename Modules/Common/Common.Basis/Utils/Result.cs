using Common.Basis.Error;
using Common.Basis.Interface;

namespace Common.Basis.Utils;

public class Result : IResult
{
    public bool IsSuccess { get; private set; }

    public bool IsFailure => !IsSuccess;

    public IError Error { get; }


    protected internal Result(bool isSuccess, IError error)
    {
        if (isSuccess && error.Type != DomainError.None.Type)
            throw new InvalidOperationException();
        if (!isSuccess && error.Type == DomainError.None.Type)
            throw new InvalidOperationException();

        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Ok() => new(true, DomainError.None);
    public static Result<T> Ok<T>(T value) => new(value, true, DomainError.None);
    public static Result Fail(IError error) => new(false, error);
    public static Result<T> Fail<T>(IError error)
    {
        return new Result<T>(default!, false, error);
    }
}

public class Result<T> : Result
{
    private readonly T _value;
    public T Value => IsSuccess ? _value : throw new InvalidOperationException("Can not access to value result");
    internal Result(T value, bool isSuccess, IError error) : base(isSuccess, error)
    {
        _value = value;
    }
}