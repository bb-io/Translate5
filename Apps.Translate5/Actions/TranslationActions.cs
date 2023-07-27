using Apps.Translate5.Dtos;
using Apps.Translate5.Models.Tasks.Requests;
using Apps.Translate5.Models;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Translate5.Models.Translations.Requests;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using Apps.Translate5.Models.Translations.Response;
using Newtonsoft.Json;

namespace Apps.Translate5.Actions
{
    [ActionList]
    public class TranslationActions
    {
        [Action("Translate text with translate5 language resources", Description = "Translate text with translate5 language resources")]
        public TranslationTextDto TranslateTextInstantly(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
           [ActionParameter] TranslateTextInstantlyRequest input)
        {
            var tr5Client = new Translate5Client(authenticationCredentialsProviders);
            var request = new Translate5Request($"/editor/instanttranslateapi/translate",
                Method.Post, authenticationCredentialsProviders);
            request.AlwaysMultipartFormData = true;

            request.AddParameter("source", input.SourceLanguage);
            request.AddParameter("target", input.TargetLanguage);
            request.AddParameter("text", input.Text);

            var intantTranslateResponse = JObject.Parse(tr5Client.Execute(request).Content);
            var languageResourceExists = intantTranslateResponse.First.First.ToObject<JObject>().ContainsKey(input.LanguageResource);
            if (!languageResourceExists)
            {
                throw new ArgumentException($"\"{input.LanguageResource}\" language resource is not configured");
            }
            var translations = intantTranslateResponse.First.First[input.LanguageResource].First.First.ToArray();
            return translations.First().ToObject<TranslationTextDto>();
        }

        [Action("Translate a file with translate5 language resources", Description = "Translate a file with translate5 language resources")]
        public TranslateFileResponse TranslateFile(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
           [ActionParameter] TranslateFileRequest input)
        {
            var tr5Client = new Translate5Client(authenticationCredentialsProviders);
            var sessionCookie = tr5Client.GetSessionCookie(authenticationCredentialsProviders);
            var request = new Translate5Request($"/editor/instanttranslateapi/filepretranslate",
                Method.Post, authenticationCredentialsProviders, sessionCookie);
            request.AlwaysMultipartFormData = true;

            request.AddParameter("source", input.SourceLanguage);
            request.AddParameter("target", input.TargetLanguage);
            request.AddFile("file", input.File, input.Filename);
            var taskId = tr5Client.Execute<TaskIdDto>(request).Data;

            var downloadUrl = tr5Client.PollFileInstantTranslation(authenticationCredentialsProviders, taskId.TaskId, sessionCookie);

            var downloadRequest = new Translate5Request(downloadUrl.Replace("\\", ""), Method.Get, authenticationCredentialsProviders, sessionCookie);
            var translatedFileResponse = tr5Client.Get(downloadRequest);

            var filename = ContentDispositionHeaderValue.Parse(translatedFileResponse.ContentHeaders.First(h => h.Name == "Content-Disposition").Value.ToString()).FileName;
            return new TranslateFileResponse()
            {
                File = translatedFileResponse.RawBytes,
                Filename = filename
            };
        }

        [Action("Write translation memory into OpenTM2", Description = "Write translation memory into OpenTM2")]
        public void WriteTranslationMemory(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
           [ActionParameter] WriteTranslationMemoryRequest input)
        {
            var tr5Client = new Translate5Client(authenticationCredentialsProviders);
            var request = new Translate5Request($"editor/instanttranslateapi/writetm", Method.Post, authenticationCredentialsProviders);
            request.AlwaysMultipartFormData = true;

            request.AddParameter("data", JsonConvert.SerializeObject(input));
            tr5Client.Execute(request);
        }
    }
}
