namespace IdContext.Infrastructure.Options;

public class ProviderClientOptions
{
    public string Name {get;set;} = string.Empty;
    public string ClientId {get;set;} = string.Empty;
    public string ClientSecret {get;set;} = string.Empty;
    public string LogoutUrl {get;set;} = string.Empty;
    public string LoginUrl {get;set;} = string.Empty;

}