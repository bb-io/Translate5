using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Net.Mime;
using Apps.Translate5.Api;
using Apps.Translate5.Extensions;
using Apps.Translate5.Invocables;
using Apps.Translate5.Models.Dtos;
using Apps.Translate5.Models.Request.Translations;
using Apps.Translate5.Models.Response;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Blackbird.Applications.Sdk.Utils.Extensions.Files;

namespace Apps.Translate5.Actions;

[ActionList]
public class TranslationActions : Translate5Invocable
{
    private readonly IFileManagementClient _fileManagementClient;
    public TranslationActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) : base(invocationContext)
    {
        _fileManagementClient = fileManagementClient;
    }

    [Action("Translate text", Description = "Translate text with translate5 language resources")]
    public async Task<TranslationTextDto> TranslateTextInstantly(
        [ActionParameter] TranslateTextInstantlyRequest input)
    {
        var parameters = new List<KeyValuePair<string, string>>()
        {
            new("source", input.SourceLanguage),
            new("target", input.TargetLanguage),
            new("text", input.Text),
        };

        var request = new Translate5Request("/editor/instanttranslateapi/translate", Method.Post, Creds)
        {
            AlwaysMultipartFormData = true
        };
        parameters.ForEach(x => request.AddParameter(x.Key, x.Value));

        var response = await Client.ExecuteWithErrorHandling(request);
        var instantTranslateResponse = JObject.Parse(response.Content);

        if (!instantTranslateResponse["rows"].Value<JObject>().ContainsKey(input.LanguageResource))
            throw new ArgumentException($"\"{input.LanguageResource}\" language resource is not configured");
        
        var translations = instantTranslateResponse.First.First[input.LanguageResource].First?.First?.Value<JArray>();

        return translations?.FirstOrDefault()?.ToObject<TranslationTextDto>() ??
               throw new("No translations from this resource");
    }

    [Action("Translate file", Description = "Translate a file with translate5 language resources")]
    public async Task<DownloadFileResponse> TranslateFile([ActionParameter] TranslateFileRequest input)
    {
        var parameters = new List<KeyValuePair<string, string>>()
        {
            new("source", input.SourceLanguage),
            new("target", input.TargetLanguage),
        };

        var sessionCookie = await Client.GetSessionCookie(Creds);

        var endpoint = "/editor/instanttranslateapi/filepretranslate";
        var request = new Translate5Request(endpoint, Method.Post, Creds, sessionCookie)
        {
            AlwaysMultipartFormData = true
        };

        parameters.ForEach(x => request.AddParameter(x.Key, x.Value));
        var fileBytes = _fileManagementClient.DownloadAsync(input.File).Result.GetByteData().Result;
        request.AddFile("file", fileBytes, input.Filename ?? input.File.Name);

        var taskId = await Client.ExecuteWithErrorHandling<TaskIdDto>(request);

        var downloadUrl = Client.PollFileInstantTranslation(Creds, taskId.TaskId, sessionCookie);

        var downloadEndpoint = downloadUrl.Replace("\\", "");
        var downloadRequest = new Translate5Request(downloadEndpoint, Method.Get, Creds, sessionCookie);

        var translatedFileResponse = await Client.ExecuteWithErrorHandling(downloadRequest);

        var filename = ContentDispositionHeaderValue
            .Parse(translatedFileResponse.ContentHeaders
                .First(h => h.Name == "Content-Disposition").Value.ToString()).FileName;

        using var stream = new MemoryStream(translatedFileResponse.RawBytes);
        var file = await _fileManagementClient.UploadAsync(stream, translatedFileResponse.ContentType ?? MediaTypeNames.Application.Octet, filename);
        return new()
        {
            File = file
        };
    }

    [Action("Write translation memory", Description = "Write translation memory into OpenTM2")]
    public Task WriteTranslationMemory([ActionParameter] WriteTranslationMemoryRequest input)
    {
        var request = new Translate5Request($"editor/instanttranslateapi/writetm", Method.Post, Creds)
        {
            AlwaysMultipartFormData = true
        }.WithData(input);

        return Client.ExecuteWithErrorHandling(request);
    }
}