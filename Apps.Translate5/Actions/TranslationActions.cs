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
    }
}
