using Common.Basis.Interface;

namespace Common.Basis.Error;

public record DomainError : IError
{
    public string Message { get; }
    public string Type { get; }

    public DomainError(string type, string message)
    {
        Type = type;
        Message = message.ToLower().Trim();
    }

    public static readonly DomainError None = new(ErrorTypes.Empty, string.Empty);
    public static readonly DomainError NullValue = new(ErrorTypes.NullValue, "The value is null");
}

public record ErrorTypes
{
    public const string NullValue = "NullValue";
    public const string Empty = "";
    public static string TypeBuilder(string context ,string local)
    {
        return $"{context}.{local}";
    }
}