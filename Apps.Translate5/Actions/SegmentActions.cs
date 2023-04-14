using Apps.Translate5.Dtos;
using Apps.Translate5.Models.Tasks.Requests;
using Apps.Translate5.Models.Tasks.Responses;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RestSharp;
using Apps.Translate5.Models.Segments.Requests;
using Apps.Translate5.Models.Segments.Responses;
using Apps.Translate5.Models;

namespace Apps.Translate5.Actions
{
    [ActionList]
    public class SegmentActions
    {

        [Action("List segments for task", Description = "List segments for task")]
        public ListSegmentsResponse ListSegments(string url, AuthenticationCredentialsProvider authenticationCredentialsProvider,
            [ActionParameter] ListSegmentsRequest input)
        {
            var sessionId = GetSessionWithEditableTask(url, authenticationCredentialsProvider, input.TaskId);
            var tr5Client = new Translate5Client(url);
            var request = new Translate5Request($"/editor/taskid/{input.TaskId}/segment?start={input.StartIndex}&limit={input.Limit}",
                Method.Get, sessionId, url);
            return new ListSegmentsResponse()
            {
                Segments = tr5Client.Get<ResponseWrapper<List<SegmentDto>>>(request).Rows
            };
        }

        [Action("Get segment", Description = "Get segment by Id")]
        public SegmentDto GetSegment(string url, AuthenticationCredentialsProvider authenticationCredentialsProvider,
            [ActionParameter] GetSegmentRequest input)
        {
            var sessionId = GetSessionWithEditableTask(url, authenticationCredentialsProvider, input.TaskId);
            var tr5Client = new Translate5Client(url);
            var request = new Translate5Request($"/editor/taskid/{input.TaskId}/segment?id={input.SegmentId}",
                Method.Get, sessionId, url);

            return tr5Client.Get<ResponseWrapper<SegmentDto>>(request).Rows;
        }

        [Action("Search segments", Description = "Search segments in task")]
        public SearchSegmentResponse SearchSegments(string url, AuthenticationCredentialsProvider authenticationCredentialsProvider,
            [ActionParameter] SearchSegmentRequest input)
        {
            var sessionId = GetSessionWithEditableTask(url, authenticationCredentialsProvider, input.TaskId);
            var tr5Client = new Translate5Client(url);
            var request = new Translate5Request($"/editor/taskid/{input.TaskId}/segment/search",
                Method.Get, sessionId, url);

            request.AddParameter("taskGuid", input.TaskGuid);
            request.AddParameter("searchField", input.SearchFieldValue);
            request.AddParameter("searchInField", input.SearchInField);

            return new SearchSegmentResponse()
            {
                Segments = tr5Client.Get<ResponseWrapper<List<SegmentSearchDto>>>(request).Rows
            };
        }

        [Action("Translate segment", Description = "Translate segment")]
        public void TranslateSegment(string url, AuthenticationCredentialsProvider authenticationCredentialsProvider,
            [ActionParameter] TranslateSegmentRequest input)
        {
            var sessionId = GetSessionWithEditableTask(url, authenticationCredentialsProvider, input.TaskId);
            var tr5Client = new Translate5Client(url);
            var request = new Translate5Request($"/editor/taskid/{input.TaskId}/segment/{input.SegmentId}",
                Method.Put, sessionId, url);
            request.AddParameter("data", JsonConvert.SerializeObject(new
            {
                targetEdit = input.Translation
            }));
            tr5Client.Execute(request);
        }

        private string GetSessionWithEditableTask(string url, AuthenticationCredentialsProvider authenticationCredentialsProvider, 
            string taskId)
        {
            var tr5Client = new Translate5Client(url);
            var request = new Translate5Request($"/editor/task/{taskId}",
                Method.Put, authenticationCredentialsProvider);
            request.AddParameter("data", JsonConvert.SerializeObject(new
            {
                userState = "edit"
            }));
            var response = tr5Client.Execute(request);
            return response.Cookies.Where(c => c.Name == "zfExtended").First().Value;
        }
    }
}
