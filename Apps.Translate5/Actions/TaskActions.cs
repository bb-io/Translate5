﻿using Blackbird.Applications.Sdk.Common;
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

namespace Apps.Translate5.Actions;

[ActionList]
public class TaskActions : Translate5Invocable
{
    public TaskActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    [Action("List tasks", Description = "List all tasks")]
    public async Task<AllTasksResponse> ListAllTasks()
    {
        var request = new Translate5Request("/editor/task", Method.Get, Creds);
        var items = await Client.Paginate<TaskDto>(request, x => x.Id);
        
        return new(items);
    }

    [Action("Get task", Description = "Get specific task")]
    public Task<TaskDto> GetTask([ActionParameter] TaskRequest input)
    {
        var endpoint = $"/editor/task/{input.TaskId}";
        var request = new Translate5Request(endpoint, Method.Get, Creds);

        return Client.ExecuteWithErrorHandling<TaskDto>(request);
    }

    [Action("Create task", Description = "Create new task")]
    public Task<TaskDto> CreateTask([ActionParameter] CreateTaskRequest input)
    {
        var parameters = new List<KeyValuePair<string, string>>()
        {
            new("taskName", input.TaskName),
            new("sourceLang", input.SourceLanguage),
            new("targetLang", input.TargetLanguage),
        };

        var request = new Translate5Request("/editor/task", Method.Post, Creds)
        {
            AlwaysMultipartFormData = true
        };

        request.AddFile("importUpload", input.File.Bytes, input.FileName ?? input.File.Name);
        parameters.ForEach(x => request.AddParameter(x.Key, x.Value));

        return Client.ExecuteWithErrorHandling<TaskDto>(request);
    }

    [Action("Change task name", Description = "Change task name")]
    public Task<TaskDto> ChangeTaskName(
        [ActionParameter] TaskRequest task,
        [ActionParameter] ChangeTaskNameRequest input)
    {
        var endpoint = $"/editor/task/{task.TaskId}";

        var request = new Translate5Request(endpoint, Method.Put, Creds)
            .WithData(input);

        return Client.ExecuteWithErrorHandling<TaskDto>(request);
    }

    [Action("Delete task", Description = "Delete task")]
    public Task DeleteTask([ActionParameter] TaskRequest input)
    {
        var endpoint = $"/editor/task/{input.TaskId}";
        var request = new Translate5Request(endpoint, Method.Delete, Creds);

        return Client.ExecuteWithErrorHandling(request);
    }

    [Action("Export translated file", Description = "Export translated file by task ID")]
    public async Task<DownloadFileResponse> ExportTaskFile([ActionParameter] TaskRequest input)
    {
        var endpoint = $"/editor/task/export/id/{input.TaskId}?format=filetranslation";
        var request = new Translate5Request(endpoint, Method.Get, Creds);

        var response = await Client.ExecuteWithErrorHandling(request);

        var filenameHeader = response.ContentHeaders.First(h => h.Name == "Content-Disposition");
        var filename = filenameHeader.Value.ToString().Split(';')[2].Split("filename=")[1];

        return new()
        {
            File = new(response.RawBytes)
            {
                Name = filename,
                ContentType = response.ContentType ?? MediaTypeNames.Application.Octet
            }
        };
    }

    [Action("Create task from ZIP", Description = "Create task from ZIP")]
    public async Task<TaskDto> CreateTaskFromZip([ActionParameter] CreateTaskFromZipRequest input)
    {
        using var memoryStream = new MemoryStream();
        using var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true);

        foreach (var workfile in input.Workfiles)
            await archive.AddFileToZip($"workfiles/{workfile.Name}", workfile.Bytes);

        if (input.Images != null)
            foreach (var image in input.Images)
                await archive.AddFileToZip($"visual/image/{image.Name}", image.Bytes);

        return await CreateTask(new()
        {
            SourceLanguage = input.SourceLanguage,
            TargetLanguage = input.TargetLanguage,
            TaskName = input.TaskName,
            FileName = "import.zip",
            File = new(memoryStream.ToArray())
        });
    }
}