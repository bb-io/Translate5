using Apps.Translate5.Api;
using Apps.Translate5.Extensions;
using Apps.Translate5.Invocables;
using Apps.Translate5.Models.Dtos;
using Apps.Translate5.Models.Request.Comments;
using Apps.Translate5.Models.Request.Segments;
using Apps.Translate5.Models.Response.Comments;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Translate5.Actions;

[ActionList]
public class CommentActions : Translate5Invocable
{
    public CommentActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    [Action("List comments", Description = "List comments for a segment")]
    public async Task<ListCommentsResponse> ListComments([ActionParameter] SegmentRequest input)
    {
        var sessionCookie = await Client.GetTaskSessionCookie(Creds, input.TaskId);
        
        var endpoint = $"/editor/taskid/{input.TaskId}/comment?segmentId={input.SegmentId}";
        var request = new Translate5Request(endpoint, Method.Get, Creds, sessionCookie);

        var response = await Client.ExecuteWithErrorHandling<List<CommentDto>>(request);
        return new(response);
    }

    [Action("Create comment", Description = "Create a new comment")]
    public async Task<CommentDto> CreateComment([ActionParameter] CreateCommentInput input)
    {
        var sessionCookie = await Client.GetTaskSessionCookie(Creds, input.TaskId);
        
        var endpoint = $"/editor/taskid/{input.TaskId}/comment";
        var request = new Translate5Request(endpoint, Method.Post, Creds, sessionCookie)
            .WithData(new CreateCommentRequest(input));

        return await Client.ExecuteWithErrorHandling<CommentDto>(request);
    }

    [Action("Delete comment", Description = "Delete specific comment")]
    public async Task DeleteComment([ActionParameter] DeleteCommentRequest input)
    {
        var sessionCookie = await Client.GetTaskSessionCookie(Creds, input.TaskId);
        
        var endpoint = $"/editor/taskid/{input.TaskId}/comment/{input.CommentId}";
        var request = new Translate5Request(endpoint, Method.Delete, Creds, sessionCookie);

        await Client.ExecuteWithErrorHandling(request);
    }
}