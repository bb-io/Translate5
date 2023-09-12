using Blackbird.Applications.Sdk.Common;

namespace Apps.Translate5.Models.Request.Tasks;

public class ChangeTaskNameRequest
{
    [Display("New task name")] public string TaskName { get; set; }
}