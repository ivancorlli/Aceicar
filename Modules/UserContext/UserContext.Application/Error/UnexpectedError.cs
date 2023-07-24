using Common.Basis.Interface;

namespace UserContext.Application.Error;

public class UnexpectedError : IError
{
    public string Message {get;private set;} = default!;

    public string Type {get;} = $"{nameof(UserContext.Application)}.{nameof(UnexpectedError)}";

    public static UnexpectedError Create(string? message)
    {
        return new UnexpectedError(){
            Message = message?? "Unexpected error on server"
        };
    }
}