using Blackbird.Applications.Sdk.Common.Authentication;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Translate5
{
    public class Translate5Request : RestRequest
    {
        public Translate5Request(string endpoint, Method method, IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders) : base(endpoint, method)
        {
            var key = authenticationCredentialsProviders.First(p => p.KeyName == "apiKey").Value;
            this.AddHeader("Translate5AuthToken", key);
            this.AddHeader("Accept", "application/json");
        }

        public Translate5Request(string endpoint, Method method, IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, 
            string sessionCookie) : base(endpoint, method)
        {
            var url = authenticationCredentialsProviders.First(p => p.KeyName == "url").Value;
            this.AddCookie("zfExtended", sessionCookie, "/", new Uri(url).Host);
            this.AddHeader("Accept", "application/json");
        }
    }
}
