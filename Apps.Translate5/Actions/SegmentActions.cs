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

namespace Apps.Translate5.Actions
{
    [ActionList]
    public class SegmentActions
    {
        private const string TestingCookie = "mfijlr4lsatnd34r2bc9au28fe";

        [Action("List segments for task", Description = "List segments for task")] //Needs PUT pre request
        public ListSegmentsResponse ListSegments(string url, AuthenticationCredentialsProvider authenticationCredentialsProvider,
            [ActionParameter] ListSegmentsRequest input)
        {
            var tr5Client = new Translate5Client(url);
            var request = new Translate5Request($"/editor/taskid/{input.TaskId}/segment?start={input.StartIndex}&limit={input.Limit}",
                Method.Get, TestingCookie, url);

            dynamic content = JsonConvert.DeserializeObject(tr5Client.Get(request).Content);
            JArray segmentsArray = content.rows;
            var segments = segmentsArray.ToObject<List<SegmentDto>>();
            return new ListSegmentsResponse()
            {
                Segments = segments
            };
        }

        [Action("Get segment", Description = "Get segment by Id")] //Needs PUT pre request
        public GetSegmentResponse GetSegment(string url, AuthenticationCredentialsProvider authenticationCredentialsProvider,
            [ActionParameter] GetSegmentRequest input)
        {
            var tr5Client = new Translate5Client(url);
            var request = new Translate5Request($"/editor/taskid/{input.TaskId}/segment?id={input.SegmentId}",
                Method.Get, TestingCookie, url);

            dynamic content = JsonConvert.DeserializeObject(tr5Client.Get(request).Content);
            JObject segments = content.rows;
            var segment = segments.ToObject<SegmentDto>();
            return new GetSegmentResponse()
            {
                Id = segment.Id,
                Source = segment.Source,
                TargetEdit = segment.TargetEdit,
                UserName = segment.UserName
            };
        }

        [Action("Search segments", Description = "Search segments in task")] //Needs PUT pre request
        public SearchSegmentResponse GetNextSegment(string url, AuthenticationCredentialsProvider authenticationCredentialsProvider,
            [ActionParameter] SearchSegmentRequest input)
        {
            var tr5Client = new Translate5Client(url);
            var request = new Translate5Request($"/editor/taskid/{input.TaskId}/segment/search",
                Method.Get, TestingCookie, url);

            request.AddParameter("taskGuid", input.TaskGuid);
            request.AddParameter("searchField", input.SearchField);
            request.AddParameter("searchInField", input.SearchInField);

            dynamic content = JsonConvert.DeserializeObject(tr5Client.Get(request).Content);
            JArray segments = content.rows;
            var segmentsSearched = segments.ToObject<List<SegmentSearchDto>>();
            return new SearchSegmentResponse()
            {
                Segments = segmentsSearched
            };
        }

        
    }
}
