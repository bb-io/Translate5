using Apps.Translate5.Constants;
using Apps.Translate5.Extensions;
using Apps.Translate5.Models.Dtos;
using Apps.Translate5.Models.Request;
using Apps.Translate5.Models.Response;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using Blackbird.Applications.Sdk.Utils.Extensions.String;
using Blackbird.Applications.Sdk.Utils.RestSharp;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.Translate5.Api;

public class Translate5Client : BlackBirdRestClient
{
    private const int Limit = 20;

    public Translate5Client(AuthenticationCredentialsProvider[] creds) : base(new()
    {
        BaseUrl = creds.Get(CredsNames.Url).Value.ToUri()
    })
    {
    }

    #region Cookies

    public async Task<string> GetSessionCookie(AuthenticationCredentialsProvider[] creds)
    {
        var configRequest = new Translate5Request("/editor/config", Method.Get, creds);
        var response = await ExecuteWithErrorHandling(configRequest);

        return response.Cookies?.FirstOrDefault(c => c.Name == "zfExtended")?.Value ??
               throw new("No session cookie received from the request");
    }

    public async Task<string> GetTaskSessionCookie(AuthenticationCredentialsProvider[] creds, string taskId)
    {
        var request = new Translate5Request($"/editor/task/{taskId}", Method.Put, creds)
            .WithData(new UserStateRequest("edit"));

        var response = await ExecuteWithErrorHandling(request);
        return response.Cookies?.FirstOrDefault(c => c.Name == "zfExtended")?.Value
               ?? throw new("No session cookie received from the request");
    }

    #endregion

    #region Requests

    public async Task<List<T>> Paginate<T>(RestRequest request, Func<T, string> distinctCallback)
    {
        var result = new List<T>();
        var url = request.Resource.SetQueryParameter("limit", Limit.ToString());

        var offset = -1;
        PaginationResponse<T> response;

        do
        {
            request.Resource = url.SetQueryParameter("start", offset.ToString());
            response = await base.ExecuteWithErrorHandling<PaginationResponse<T>>(request);

            offset += Limit;
            result.AddRange(response.Rows);
        } while (response.Total > result.Count);

        return result.DistinctBy(distinctCallback).ToList();
    }

    public override async Task<T> ExecuteWithErrorHandling<T>(RestRequest request)
    {
        var response = await base.ExecuteWithErrorHandling<T>(request);
        return response;
    }

    #endregion

    #region Utils

    public string PollFileInstantTranslation(AuthenticationCredentialsProvider[] creds,
        string taskId,
        string sessionCookie)
    {
        var pollFileList = new Translate5Request("/editor/instanttranslateapi/filelist", Method.Get,
            creds, sessionCookie);

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

    protected override Exception ConfigureErrorException(RestResponse response)
    {
        if (string.IsNullOrWhiteSpace(response.Content))
            throw new PluginApplicationException(response.ErrorMessage);

        var error = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);
        if (error == null || string.IsNullOrWhiteSpace(error.ErrorMessage))
            return new PluginApplicationException($"Status code: {response.StatusCode}, Content: {response.Content}");

        return new PluginApplicationException($"Error: {error.ErrorMessage}");
    }
    #endregion
}