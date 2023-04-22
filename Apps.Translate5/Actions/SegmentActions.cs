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
using Blackbird.Applications.Sdk.Common.Actions;

namespace Apps.Translate5.Actions
{
    [ActionList]
    public class SegmentActions
    {

        [Action("List segments for task", Description = "List segments for task")]
        public ListSegmentsResponse ListSegments(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] ListSegmentsRequest input)
        {
            var tr5Client = new Translate5Client(authenticationCredentialsProviders);
            var request = new Translate5EditRequest($"/editor/taskid/{input.TaskId}/segment?start={input.StartIndex}&limit={input.Limit}", Method.Get, authenticationCredentialsProviders, input.TaskId);
            return new ListSegmentsResponse()
            {
                Segments = tr5Client.Get<ResponseWrapper<List<SegmentDto>>>(request).Rows
            };
        }

        [Action("Get segment", Description = "Get segment by Id")]
        public SegmentDto GetSegment(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] GetSegmentRequest input)
        {
            var tr5Client = new Translate5Client(authenticationCredentialsProviders);
            var request = new Translate5EditRequest($"/editor/taskid/{input.TaskId}/segment?id={input.SegmentId}",
                Method.Get, authenticationCredentialsProviders, input.TaskId);

            return tr5Client.Get<ResponseWrapper<SegmentDto>>(request).Rows;
        }

        [Action("Search segments", Description = "Search segments in task")]
        public SearchSegmentResponse SearchSegments(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] SearchSegmentRequest input)
        {
            var tr5Client = new Translate5Client(authenticationCredentialsProviders);
            var request = new Translate5EditRequest($"/editor/taskid/{input.TaskId}/segment/search", Method.Get, authenticationCredentialsProviders, input.TaskId);

            request.AddParameter("taskGuid", input.TaskGuid);
            request.AddParameter("searchField", input.SearchFieldValue);
            request.AddParameter("searchInField", input.SearchInField);

            return new SearchSegmentResponse()
            {
                Segments = tr5Client.Get<ResponseWrapper<List<SegmentSearchDto>>>(request).Rows
            };
        }

        [Action("Translate segment", Description = "Translate segment")]
        public void TranslateSegment(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] TranslateSegmentRequest input)
        {
            var tr5Client = new Translate5Client(authenticationCredentialsProviders);
            var request = new Translate5EditRequest($"/editor/taskid/{input.TaskId}/segment/{input.SegmentId}",
                Method.Put, authenticationCredentialsProviders, input.TaskId);
            request.AddParameter("data", JsonConvert.SerializeObject(new
            {
                targetEdit = input.Translation
            }));
            tr5Client.Execute(request);
        }

        
    }
}
