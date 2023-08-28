namespace Apps.Translate5.Models.Segments.Requests;

public class ListSegmentsRequest
{
    public string TaskId { get; set; }

    public int StartIndex { get; set; }

    public int Limit { get; set; }
}