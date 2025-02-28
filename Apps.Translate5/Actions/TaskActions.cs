using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using RestSharp;
using System.IO.Compression;
using System.Net.Mime;
using Apps.Translate5.Api;
using Apps.Translate5.Extensions;
using Apps.Translate5.Invocables;
using Apps.Translate5.Models.Dtos;
using Apps.Translate5.Models.Request.Tasks;
using Apps.Translate5.Models.Response;
using Apps.Translate5.Models.Response.Tasks;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Blackbird.Applications.Sdk.Utils.Extensions.Files;
using System.IO;
using Blackbird.Applications.Sdk.Common.Exceptions;

namespace Apps.Translate5.Actions;

[ActionList]
public class TaskActions : Translate5Invocable
{
    private readonly IFileManagementClient _fileManagementClient;
    public TaskActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) : base(invocationContext)
    {
        _fileManagementClient = fileManagementClient;
    }

    [Action("List tasks", Description = "List all tasks")]
    public async Task<AllTasksResponse> ListAllTasks()
    {
        var request = new Translate5Request("/editor/task", Method.Get, Creds);
        var items = await Client.Paginate<TaskDto>(request, x => x.Id);
        return new(items);
    }

    [Action("Get task", Description = "Get specific task")]
    public async Task<TaskDto> GetTask([ActionParameter] TaskRequest input)
    {
        var endpoint = $"/editor/task/{input.TaskId}";
        var request = new Translate5Request(endpoint, Method.Get, Creds);
        var response = await Client.ExecuteWithErrorHandling<ResponseWrapper<TaskDto>>(request);
        return response.Rows;
    }

    [Action("Create task", Description = "Create new task")]
    public async Task<TaskDto> CreateTask([ActionParameter] CreateTaskRequest input)
    {
        var parameters = new List<KeyValuePair<string, string?>>
        {
            new("taskName", input.TaskName),
            new("sourceLang", input.SourceLanguage),
            new("targetLang", input.TargetLanguage),
            new("customerId", input.CustomerId),
            new("foreignId", input.ForeignId),
            new("foreignName", input.ForeignName),
        };

        var request = new Translate5Request("/editor/task", Method.Post, Creds)
        {
            AlwaysMultipartFormData = true
        };

        var fileBytes = _fileManagementClient.DownloadAsync(input.File).Result.GetByteData().Result;
        request.AddFile("importUpload", fileBytes, input.FileName ?? input.File.Name);
        parameters
            .Where(x => x.Value is not null)
            .ToList()
            .ForEach(x => request.AddParameter(x.Key, x.Value));

        var response = await Client.ExecuteWithErrorHandling<ResponseWrapper<TaskDto>>(request);
        return response.Rows;
    }

    [Action("Change task name", Description = "Change task name")]
    public async Task<TaskDto> ChangeTaskName(
        [ActionParameter] TaskRequest task,
        [ActionParameter] ChangeTaskNameRequest input)
    {
        var endpoint = $"/editor/task/{task.TaskId}";

        var request = new Translate5Request(endpoint, Method.Put, Creds)
            .WithData(input);

        var response = await Client.ExecuteWithErrorHandling<ResponseWrapper<TaskDto>>(request);
        return response.Rows;
    }

    [Action("Delete task", Description = "Delete task")]
    public async Task DeleteTask([ActionParameter] TaskRequest input)
    {
        var endpoint = $"/editor/task/{input.TaskId}";
        var request = new Translate5Request(endpoint, Method.Delete, Creds);
        await Client.ExecuteWithErrorHandling(request);
    }

    [Action("Export translated file", Description = "Export translated file by task ID")]
    public async Task<DownloadFileResponse> ExportTaskFile([ActionParameter] TaskRequest input)
    {
        var endpoint = $"/editor/task/export/id/{input.TaskId}?format=filetranslation";
        var request = new Translate5Request(endpoint, Method.Get, Creds);

        var response = await Client.ExecuteWithErrorHandling(request);

        var filenameHeader = response.ContentHeaders.First(h => h.Name == "Content-Disposition");
        string filename;
        try
        {
            filename = filenameHeader.Value.ToString().Split(';')[2].Split("filename=")[1];
        }
        catch (IndexOutOfRangeException ex)
        {
            throw new PluginApplicationException("Unexpected format response received from server. Please check the input and try again");
        }

        using var stream = new MemoryStream(response.RawBytes);
        var file = await _fileManagementClient.UploadAsync(stream, response.ContentType ?? MediaTypeNames.Application.Octet, filename);
        return new()
        {
            File = file
        };
    }

    [Action("Create task from ZIP", Description = "Create task from ZIP")]
    public async Task<TaskDto> CreateTaskFromZip([ActionParameter] CreateTaskFromZipRequest input)
    {
        using var memoryStream = new MemoryStream();
        using var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true);

        foreach (var workfile in input.Workfiles)
        {
            var fileBytes = _fileManagementClient.DownloadAsync(workfile).Result.GetByteData().Result;
            await archive.AddFileToZip($"workfiles/{workfile.Name}", fileBytes);
        }    

        if (input.Images != null)
        {
            foreach (var image in input.Images)
            {
                var fileBytes = _fileManagementClient.DownloadAsync(image).Result.GetByteData().Result;
                await archive.AddFileToZip($"visual/image/{image.Name}", fileBytes);
            }  
        }
        var file = await _fileManagementClient.UploadAsync(memoryStream, MediaTypeNames.Application.Zip, "import.zip");
        return await CreateTask(new()
        {
            SourceLanguage = input.SourceLanguage,
            TargetLanguage = input.TargetLanguage,
            TaskName = input.TaskName,
            FileName = "import.zip",
            File = file
        });
    }
}