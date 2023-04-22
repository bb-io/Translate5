using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Translate5
{
    public class Translate5Client : RestClient
    {
        private static Uri GetUri(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders)
        {
            var url = authenticationCredentialsProviders.First(p => p.KeyName == "url").Value;
            return new Uri(url);
        }

        public Translate5Client(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders) : base(new RestClientOptions() { ThrowOnAnyError = true, BaseUrl = GetUri(authenticationCredentialsProviders) }) { }
    }
}
