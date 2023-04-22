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
    public class Translate5EditRequest : RestRequest
    {
        public Translate5EditRequest(string endpoint, Method method, IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, string taskId) : base(endpoint, method)
        {
            var url = authenticationCredentialsProviders.First(p => p.KeyName == "url").Value;
            var tr5Client = new Translate5Client(authenticationCredentialsProviders);
            var request = new Translate5Request($"/editor/task/{taskId}",
                Method.Put, authenticationCredentialsProviders);
            request.AddParameter("data", JsonConvert.SerializeObject(new
            {
                userState = "edit"
            }));
            var response = tr5Client.Execute(request);
            var zfExtendedCookie = response.Cookies.Where(c => c.Name == "zfExtended").First().Value;

            this.AddCookie("zfExtended", zfExtendedCookie, "/", new Uri(url).Host);
            this.AddHeader("Accept", "application/json");
        }
    }
}
