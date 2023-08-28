namespace Apps.Translate5.Models.Tasks.Requests;

public class ChangeTaskNameRequest
{
    public string TaskId { get; set; }

    public string NewName { get; set; }
}