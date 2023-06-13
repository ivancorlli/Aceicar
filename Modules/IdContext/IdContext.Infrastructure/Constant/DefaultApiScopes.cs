namespace IdContext.Infrastructure.Constant;



public static class DefaultApiScopes
{
    public const string Api = "api";

    
}

public static class ApiScopesList
{
    public static HashSet<string> Scopes = new(){
        DefaultApiScopes.Api
    };
}