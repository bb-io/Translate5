using Apps.Translate5.Dtos;
using Apps.Translate5.Models.Tasks.Requests;
using Apps.Translate5.Models.Tasks.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Translate5.Models.Users.Responses;
using Apps.Translate5.Models.Users.Requests;

namespace Apps.Translate5.Actions
{
    [ActionList]
    public class UserActions
    {
        [Action("List all users", Description = "List all users")]
        public AllUsersResponse ListAllUsers(string url, AuthenticationCredentialsProvider authenticationCredentialsProvider,
            [ActionParameter] AllUsersRequest input)
        {
            var tr5Client = new Translate5Client(url);
            var request = new Translate5Request($"/editor/user",
                Method.Get, authenticationCredentialsProvider);
            dynamic content = JsonConvert.DeserializeObject(tr5Client.Get(request).Content);
            JArray usersArray = content.rows;
            var users = usersArray.ToObject<List<UserDto>>();
            return new AllUsersResponse()
            {
                Users = users
            };
        }
    }
}
