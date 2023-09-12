using Apps.Translate5.DataSourceHandlers;
using Apps.Translate5.DataSourceHandlers.EnumHandlers;
using Apps.Translate5.Models.Request.Tasks;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Translate5.Models.Request.Segments;

public class SearchSegmentRequest : TaskRequest
{
    [Display("Task GUID")]
    [DataSource(typeof(TaskGuidDataHandler))]
    public string TaskGuid { get; set; }

    [Display("Search field")]
    public string SearchField { get; set; }

    [Display("Search in field")]
    [DataSource(typeof(SearchInDataHandler))]
    public string SearchInField { get; set; }
}