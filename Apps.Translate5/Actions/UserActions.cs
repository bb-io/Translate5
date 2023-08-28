using Apps.Translate5.Dtos;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;
using Apps.Translate5.Models.Users.Responses;
using Apps.Translate5.Models.Users.Requests;
using Apps.Translate5.Models;
using Blackbird.Applications.Sdk.Common.Actions;

namespace Apps.Translate5.Actions;

[ActionList]
public class UserActions
{
    [Action("List all users", Description = "List all users")]
    public AllUsersResponse ListAllUsers(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] AllUsersRequest input)
    {
        var tr5Client = new Translate5Client(authenticationCredentialsProviders);
        var request = new Translate5Request($"/editor/user",
            Method.Get, authenticationCredentialsProviders);
        return new AllUsersResponse()
        {
            Users = tr5Client.Get<ResponseWrapper<List<UserDto>>>(request).Rows
        };
    }
}