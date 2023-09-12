using Apps.Translate5.Api;
using Apps.Translate5.Extensions;
using Apps.Translate5.Invocables;
using Blackbird.Applications.Sdk.Common;
using RestSharp;
using Apps.Translate5.Models.Dtos;
using Apps.Translate5.Models.Dtos.Simple;
using Apps.Translate5.Models.Request.Segments;
using Apps.Translate5.Models.Request.Tasks;
using Apps.Translate5.Models.Response.Segments;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Translate5.Actions;

[ActionList]
public class SegmentActions : Translate5Invocable
{
    public SegmentActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    [Action("List segments", Description = "List segments for a task")]
    public async Task<ListSegmentsResponse> ListSegments([ActionParameter] TaskRequest input)
    {
        var sessionCookie = await Client.GetTaskSessionCookie(Creds, input.TaskId);
        
        var endpoint = $"/editor/taskid/{input.TaskId}/segment";
        var request = new Translate5Request(endpoint, Method.Get, Creds, sessionCookie);

        var response = await Client.Paginate<SegmentDto>(request, x => x.Id);
        return new(response);
    }

    [Action("Get segment", Description = "Get specific segment")]
    public async Task<SegmentDto> GetSegment([ActionParameter] SegmentRequest input)
    {
        var sessionCookie = await Client.GetTaskSessionCookie(Creds, input.TaskId);
        
        var endpoint = $"/editor/taskid/{input.TaskId}/segment?id={input.SegmentId}";
        var request = new Translate5Request(endpoint, Method.Get, Creds, sessionCookie);

        return await Client.ExecuteWithErrorHandling<SegmentDto>(request);
    }

    [Action("Search segments", Description = "Search segments in task")]
    public async Task<SearchSegmentResponse> SearchSegments([ActionParameter] SearchSegmentRequest input)
    {
        var sessionCookie = await Client.GetTaskSessionCookie(Creds, input.TaskId);

        var parameters = new List<KeyValuePair<string, string>>()
        {
            new("taskGuid", input.TaskGuid),
            new("searchField", input.SearchField),
            new("searchInField", input.SearchInField),
        };

        var endpoint = $"/editor/taskid/{input.TaskId}/segment/search";
        var request = new Translate5Request(endpoint, Method.Get, Creds, sessionCookie);

        parameters.ForEach(x => request.AddParameter(x.Key, x.Value));

        var response = await Client.ExecuteWithErrorHandling<List<SimpleSegmentDto>>(request);
        return new(response);
    }

    [Action("Translate segment", Description = "Translate a specific segment")]
    public Task TranslateSegment(
        [ActionParameter] SegmentRequest segment,
        [ActionParameter] TranslateSegmentRequest input)
    {
        var endpoint = $"/editor/taskid/{segment.TaskId}/segment/{segment.SegmentId}";

        var request = new Translate5Request(endpoint, Method.Put, Creds, segment.TaskId)
            .WithData(input);

        return Client.ExecuteWithErrorHandling(request);
    }
}