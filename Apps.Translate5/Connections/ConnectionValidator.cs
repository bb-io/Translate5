using Apps.Translate5.Api;
using Apps.Translate5.Constants;
using Apps.Translate5.Models.Dtos;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using Blackbird.Applications.Sdk.Utils.Extensions.String;
using RestSharp;

namespace Apps.Translate5.Connections;

public class ConnectionValidator : IConnectionValidator
{
    public async ValueTask<ConnectionValidationResponse> ValidateConnection(
        IEnumerable<AuthenticationCredentialsProvider> authProviders, CancellationToken cancellationToken)
    {
        var creds = authProviders.ToArray();
        var client = new Translate5Client(creds);

        var request = new Translate5Request("/editor/language", Method.Get, creds);

        try
        {
            var response = await client.ExecuteWithErrorHandling(request);
            
            if(response.Content != null && response.Content.Contains("null"))
            {
                return new()
                {
                    IsValid = false,
                    Message = $"Probably you have entered wrong URL, please check it and try again. URL should be like: https://[domain]"
                };
            }

            return new()
            {
                IsValid = true
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                IsValid = false,
                Message = ex.Message
            };
        }
    }
}