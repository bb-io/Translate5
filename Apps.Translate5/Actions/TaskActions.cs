
using Apps.Translate5.Dtos;
using Apps.Translate5.Models;
using Apps.Translate5.Models.Tasks.Requests;
using Apps.Translate5.Models.Tasks.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Apps.Translate5.Actions
{
    [ActionList]
    public class TaskActions
    {
        [Action("List all tasks", Description = "List all tasks")]
        public AllTasksResponse ListAllTasks(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] AllTasksRequest input)
        {
            var tr5Client = new Translate5Client(authenticationCredentialsProviders);
            var request = new Translate5Request($"/editor/task?start={input.StartIndex}&limit={input.Limit}", 
                Method.Get, authenticationCredentialsProviders);
            return new AllTasksResponse()
            {
                Tasks = tr5Client.Get<ResponseWrapper<List<TaskDto>>>(request).Rows
            };
        }

        [Action("Get task", Description = "Get task by Id")]
        public GetTaskResponse GetTask(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
           [ActionParameter] GetTaskRequest input)
        {
            var tr5Client = new Translate5Client(authenticationCredentialsProviders);
            var request = new Translate5Request($"/editor/task/{input.Id}",
                Method.Get, authenticationCredentialsProviders);
            var task = tr5Client.Get<ResponseWrapper<TaskDto>>(request).Rows;
            return new GetTaskResponse()
            {
                Id = task.Id,
                Name = task.TaskName
            };
        }

        [Action("Create task", Description = "Create new task")]
        public TaskDto CreateTask(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
           [ActionParameter] CreateTaskRequest input)
        {
            var tr5Client = new Translate5Client(authenticationCredentialsProviders);
            var request = new Translate5Request($"/editor/task",
                Method.Post, authenticationCredentialsProviders);
            request.AlwaysMultipartFormData = true;

            request.AddParameter("taskName", input.TaskName);
            request.AddParameter("sourceLang", input.SourceLanguage);
            request.AddParameter("targetLang", input.TargetLanguage);

            request.AddFile("importUpload", input.File, input.FileName);
            return tr5Client.Execute<ResponseWrapper<TaskDto>>(request).Data.Rows;
        }

       
        [Action("Change task name", Description = "Change task name")]
        public TaskDto ChangeTaskName(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
           [ActionParameter] ChangeTaskNameRequest input)
        {
            var tr5Client = new Translate5Client(authenticationCredentialsProviders);
            var request = new Translate5Request($"/editor/task/{input.TaskId}", Method.Put, authenticationCredentialsProviders);
            request.AddParameter("data", JsonConvert.SerializeObject(new
            {
                taskName = input.NewName
            }));
            return tr5Client.Execute<ResponseWrapper<TaskDto>>(request).Data.Rows;
        }

        [Action("Delete task", Description = "Delete task")]
        public void DeleteTask(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
           [ActionParameter] DeleteTaskRequest input)
        {
            var tr5Client = new Translate5Client(authenticationCredentialsProviders);
            var request = new Translate5Request($"/editor/task/{input.Id}",
                Method.Delete, authenticationCredentialsProviders);

            tr5Client.Execute(request);
        }

        [Action("Export translated file", Description = "Export translated file by task Id")]
        public ExportTaskFileResponse ExportTaskFile(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
           [ActionParameter] GetTaskRequest input)
        {
            var tr5Client = new Translate5Client(authenticationCredentialsProviders);
            var request = new Translate5Request($"/editor/task/export/id/{input.Id}?format=filetranslation",
                Method.Get, authenticationCredentialsProviders);
            var response = tr5Client.Get(request);

            var filenameHeader = response.ContentHeaders.First(h => h.Name == "Content-Disposition");
            var filename = filenameHeader.Value.ToString().Split(';')[2].Split("filename=")[1];

            return new ExportTaskFileResponse()
            {
                Filename = filename,
                File = response.RawBytes
            };
        }
    }
}
