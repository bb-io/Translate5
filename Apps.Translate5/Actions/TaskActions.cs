
using Apps.Translate5.Dtos;
using Apps.Translate5.Models.Tasks.Requests;
using Apps.Translate5.Models.Tasks.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Translate5.Actions
{
    [ActionList]
    public class TaskActions
    {
        [Action("List all tasks", Description = "List all tasks")]
        public AllTasksResponse ListAllTasks(string url, AuthenticationCredentialsProvider authenticationCredentialsProvider,
            [ActionParameter] AllTasksRequest input)
        {
            var tr5Client = new Translate5Client(url);
            var request = new Translate5Request($"/editor/task?start={input.StartIndex}&limit={input.Limit}", 
                Method.Get, authenticationCredentialsProvider);
            dynamic content = JsonConvert.DeserializeObject(tr5Client.Get(request).Content);
            JArray tasksArray = content.rows;
            var tasks = tasksArray.ToObject<List<TaskDto>>();
            return new AllTasksResponse()
            {
                Tasks = tasks
            };
        }

        [Action("Get task", Description = "Get task by Id")]
        public GetTaskResponse GetTask(string url, AuthenticationCredentialsProvider authenticationCredentialsProvider,
           [ActionParameter] GetTaskRequest input)
        {
            var tr5Client = new Translate5Client(url);
            var request = new Translate5Request($"/editor/task/{input.Id}",
                Method.Get, authenticationCredentialsProvider);
            dynamic content = JsonConvert.DeserializeObject(tr5Client.Get(request).Content);
            JObject tasksArray = content.rows;
            var task = tasksArray.ToObject<TaskDto>();
            return new GetTaskResponse()
            {
                Id = task.Id,
                Name = task.TaskName
            };
        }

        [Action("Create task", Description = "Create new task")]
        public void CreateTask(string url, AuthenticationCredentialsProvider authenticationCredentialsProvider,
           [ActionParameter] CreateTaskRequest input)
        {
            var tr5Client = new Translate5Client(url);
            var request = new Translate5Request($"/editor/task",
                Method.Post, authenticationCredentialsProvider);
            request.AlwaysMultipartFormData = true;

            request.AddParameter("taskName", input.TaskName);
            request.AddParameter("sourceLang", input.SourceLanguage);
            request.AddParameter("targetLang", input.TargetLanguage);

            request.AddFile("importUpload", input.File, input.FileName, input.FileType);
            tr5Client.Execute(request);
        }

        /* PUT requests not working
        [Action("Change task name", Description = "Change task name")]
        public void ChangeTaskName(string url, AuthenticationCredentialsProvider authenticationCredentialsProvider,
           [ActionParameter] ChangeTaskNameRequest input)
        {
            var tr5Client = new Translate5Client(url);
            var request = new Translate5Request($"/editor/task/{input.TaskId}", Method.Put, authenticationCredentialsProvider);
            request.AddJsonBody(new
            {
                taskName = input.NewName
            });
            tr5Client.Execute(request);
        }
        */


        [Action("Delete task", Description = "Delete task")]
        public void DeleteTask(string url, AuthenticationCredentialsProvider authenticationCredentialsProvider,
           [ActionParameter] DeleteTaskRequest input)
        {
            var tr5Client = new Translate5Client(url);
            var request = new Translate5Request($"/editor/task/{input.Id}",
                Method.Delete, authenticationCredentialsProvider);

            tr5Client.Execute(request);
        }
    }
}
