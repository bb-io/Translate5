using Apps.Translate5.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using Blackbird.Applications.Sdk.Utils.Extensions.String;
using RestSharp;

namespace Apps.Translate5.Api;

public class Translate5Request : RestRequest
{
    public Translate5Request(string endpoint, Method method, AuthenticationCredentialsProvider[] creds) :
        base(endpoint, method)
    {
        var key = creds.Get(CredsNames.ApiKey).Value;

        this.AddHeader("Translate5AuthToken", key);
        this.AddHeader("Accept", "application/json");
    }

    public Translate5Request(string endpoint, Method method, AuthenticationCredentialsProvider[] creds,
        string sessionCookie) : base(endpoint, method)
    {
        var url = creds.Get(CredsNames.Url).Value.ToUri();

        this.AddCookie("zfExtended", sessionCookie, "/", url.Host);
        this.AddHeader("Accept", "application/json");
    }
}