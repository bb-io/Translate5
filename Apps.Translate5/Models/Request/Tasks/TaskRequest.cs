using Apps.Translate5.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Translate5.Models.Request.Tasks;

public class TaskRequest
{
    [Display("Task ID")]
    [DataSource(typeof(TaskDataHandler))]
    public string TaskId { get; set; }
}