using Apps.Translate5.Dtos;
using Apps.Translate5.Models;
using Apps.Translate5.Models.Comments.Requests;
using Apps.Translate5.Models.Comments.Response;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.Translate5.Actions;

[ActionList]
public class CommentActions
{
    [Action("List comments for segment", Description = "List comments for segment")]
    public ListCommentsResponse ListComments(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] ListCommentsRequest input)
    {
        var tr5Client = new Translate5Client(authenticationCredentialsProviders);
        var request = new Translate5EditRequest($"/editor/taskid/{input.TaskId}/comment?segmentId={input.SegmentId}",
            Method.Get, authenticationCredentialsProviders, input.TaskId);
        return new ListCommentsResponse()
        {
            Comments = tr5Client.Get<ResponseWrapper<List<CommentDto>>>(request).Rows
        };
    }

    [Action("Create comment", Description = "Create comment")]
    public CommentDto CreateComment(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] CreateCommentRequest input)
    {
        var tr5Client = new Translate5Client(authenticationCredentialsProviders);
        var request = new Translate5EditRequest($"/editor/taskid/{input.TaskId}/comment",
            Method.Post, authenticationCredentialsProviders, input.TaskId);
        request.AddParameter("data", JsonConvert.SerializeObject(new
        {
            segmentId = input.SegmentId,
            comment = input.Comment
        }));
        return tr5Client.Execute<ResponseWrapper<CommentDto>>(request).Data.Rows;
    }

    [Action("Delete comment", Description = "Delete comment")]
    public void DeleteComment(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] DeleteCommentRequest input)
    {
        var tr5Client = new Translate5Client(authenticationCredentialsProviders);
        var request = new Translate5EditRequest($"/editor/taskid/{input.TaskId}/comment/{input.CommentId}", Method.Delete, authenticationCredentialsProviders, input.TaskId);
        tr5Client.Execute(request);
    }
}