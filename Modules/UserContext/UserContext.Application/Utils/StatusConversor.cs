using UserContext.Core.Enumerable;

namespace UserContext.Application.Utils;

public static class StatusConversor
{
    internal static string Convert(Status status)
    {
        string response = string.Empty;
        switch (status)
        {
            case Core.Enumerable.Status.Active:
                response = "Active";
                break;
            case Core.Enumerable.Status.Deleted:
                response = "Deleted";
                break;
            case Core.Enumerable.Status.Inactive:
                response = "Inactive";
                break;
            case Core.Enumerable.Status.Suspended:
                response = "Suspended";
                break;
            default:
                response = string.Empty;
                break;
        }
        return response;
    }
}