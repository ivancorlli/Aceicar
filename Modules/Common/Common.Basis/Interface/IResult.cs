namespace Common.Basis.Interface;

public interface IResult
{
    public bool IsSuccess { get; }
    public bool IsFailure { get; }
    public IError Error { get; }
}