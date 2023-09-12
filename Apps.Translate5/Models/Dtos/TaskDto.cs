using Blackbird.Applications.Sdk.Common;

namespace Apps.Translate5.Models.Dtos;

public class TaskDto
{
    [Display("Task ID")]
    public string Id { get; set; }
    
    [Display("Task GUID")]
    public string TaskGuid { get; set; }

    [Display("Task name")]
    public string TaskName { get; set; }
}