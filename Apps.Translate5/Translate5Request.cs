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
        public Translate5Request(string endpoint, Method method, AuthenticationCredentialsProvider authenticationCredentialsProvider) : base(endpoint, method)
        {
            this.AddHeader("Translate5AuthToken", authenticationCredentialsProvider.Value);
            this.AddHeader("Accept", "application/json");
        }

        public Translate5Request(string endpoint, Method method, string zfExtendedCookie, string url) : base(endpoint, method)
        {
            this.AddCookie("zfExtended", zfExtendedCookie, "/", new Uri(url).Host);
            this.AddHeader("Accept", "application/json");
        }

        public static string GetSessionWithEditableTask(string url, AuthenticationCredentialsProvider authenticationCredentialsProvider,
            string taskId)
        {
            var tr5Client = new Translate5Client(url);
            var request = new Translate5Request($"/editor/task/{taskId}",
                Method.Put, authenticationCredentialsProvider);
            request.AddParameter("data", JsonConvert.SerializeObject(new
            {
                userState = "edit"
            }));
            var response = tr5Client.Execute(request);
            return response.Cookies.Where(c => c.Name == "zfExtended").First().Value;
        }
    }
}
