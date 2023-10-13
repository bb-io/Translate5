using Apps.Translate5.Api;
using Apps.Translate5.Invocables;
using RestSharp;
using Apps.Translate5.Models.Dtos;
using Apps.Translate5.Models.Response;
using Apps.Translate5.Models.Response.Users;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Translate5.Actions;

[ActionList]
public class UserActions : Translate5Invocable
{
    public UserActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    [Action("List users", Description = "List all users")]
    public async Task<AllUsersResponse> ListAllUsers()
    {
        var request = new Translate5Request("/editor/user", Method.Get, Creds);
        var response = await Client.ExecuteWithErrorHandling<ResponseWrapper<List<UserDto>>>(request);

        return new(response.Rows);
    }
}