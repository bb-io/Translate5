using Apps.Translate5.Dtos;
using Blackbird.Applications.Sdk.Common.Authentication;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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

        public string PollFileInstantTranslation(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, string taskId, string sessionCookie)
        {
            var pollFileList = new Translate5Request("/editor/instanttranslateapi/filelist", Method.Get, authenticationCredentialsProviders, sessionCookie);

            AllPretranslatedFile fileInfo = null;
            while (fileInfo == null || fileInfo.DownloadUrl == "isImporting")
            {
                var result = this.Get(pollFileList).Content;
                var fileList = JsonConvert.DeserializeObject<FileListDto>(result);
                fileInfo = fileList.AllPretranslatedFiles.First(f => f.TaskId == taskId);
                if (fileInfo.Errors.Count > 0)
                    throw new ArgumentException("Error during file processing in translate5");
                Task.Delay(1000);
            }
            return fileInfo.DownloadUrl;
        }

        public string GetSessionCookie(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders)
        {
            var configRequest = new Translate5Request("/editor/config", Method.Get, authenticationCredentialsProviders);
            return this.Get(configRequest).Cookies.Where(c => c.Name == "zfExtended").First().Value;
        }
    }
}
