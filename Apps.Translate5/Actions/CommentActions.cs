using Apps.Translate5.Dtos;
using Apps.Translate5.Models;
using Apps.Translate5.Models.Comments.Requests;
using Apps.Translate5.Models.Comments.Response;
using Apps.Translate5.Models.Segments.Requests;
using Apps.Translate5.Models.Segments.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Translate5.Actions
{
    [ActionList]
    public class CommentActions
    {
        [Action("List comments for segment", Description = "List comments for segment")]
        public ListCommentsResponse ListComments(string url, AuthenticationCredentialsProvider authenticationCredentialsProvider,
            [ActionParameter] ListCommentsRequest input)
        {
            var tr5Client = new Translate5Client(url);
            var request = new Translate5EditRequest($"/editor/taskid/{input.TaskId}/comment?segmentId={input.SegmentId}",
                Method.Get, authenticationCredentialsProvider, url, input.TaskId);
            return new ListCommentsResponse()
            {
                Comments = tr5Client.Get<ResponseWrapper<List<CommentDto>>>(request).Rows
            };
        }

        [Action("Create comment", Description = "Create comment")]
        public void CreateComment(string url, AuthenticationCredentialsProvider authenticationCredentialsProvider,
            [ActionParameter] CreateCommentRequest input)
        {
            var tr5Client = new Translate5Client(url);
            var request = new Translate5EditRequest($"/editor/taskid/{input.TaskId}/comment",
                Method.Post, authenticationCredentialsProvider, url, input.TaskId);
            request.AddParameter("data", JsonConvert.SerializeObject(new
            {
                segmentId = input.SegmentId,
                comment = input.Comment
            }));
            tr5Client.Execute(request);
        }

        [Action("Delete comment", Description = "Delete comment")]
        public void DeleteComment(string url, AuthenticationCredentialsProvider authenticationCredentialsProvider,
            [ActionParameter] DeleteCommentRequest input)
        {
            var tr5Client = new Translate5Client(url);
            var request = new Translate5EditRequest($"/editor/taskid/{input.TaskId}/comment/{input.CommentId}", Method.Delete, authenticationCredentialsProvider, url, input.TaskId);
            tr5Client.Execute(request);
        }
    }
}
