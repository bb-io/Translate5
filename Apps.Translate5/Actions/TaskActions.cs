
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
            var projects = tasksArray.ToObject<List<TaskDto>>();
            return new AllTasksResponse()
            {
                Tasks = projects
            };
        }
    }
}
