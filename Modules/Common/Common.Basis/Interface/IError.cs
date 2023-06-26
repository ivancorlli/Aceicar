namespace Common.Basis.Interface;

public interface IError
{
    public string Message { get; }
    public string Type { get; }
}