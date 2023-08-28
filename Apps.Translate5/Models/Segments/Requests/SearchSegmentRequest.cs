namespace Apps.Translate5.Models.Segments.Requests;

public class SearchSegmentRequest
{
    public string TaskId { get; set; }

    public string TaskGuid { get; set; }

    public string SearchFieldValue { get; set; }

    public string SearchInField { get; set; } // "source" "target"
}