using Common.Basis.Interface;
using Common.Basis.Utils;
using UserContext.Core.Aggregate;
using UserContext.Core.Error;
using UserContext.Core.Repository;
using UserContext.Core.ValueObject;

namespace UserContext.Application.Feature.ApplicationUser.Command.ChangePicture;

public sealed record ChangePictureCommand(string UserId,string Picture);
public static class ChangePictureHandler
{
    public static async Task<IOperationResult> Handle(
        ChangePictureCommand command,
        IUoW session
    )
    {
        ProfileImage picture = new ProfileImage(command.Picture);
        User? user = await session.UserRepository.FindById(Guid.Parse(command.UserId));
        if(user == null) return OperationResult.NotFound(new UserNotFound().Message);
        user.ChangeImage(picture);
        session.UserRepository.Apply(user);
        return OperationResult.Success();
    }   
}