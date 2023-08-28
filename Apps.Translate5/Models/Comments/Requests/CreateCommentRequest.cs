namespace Apps.Translate5.Models.Comments.Requests;

public class CreateCommentRequest
{
    public string SegmentId { get; set; }

    public string TaskId { get; set; }

    public string Comment { get; set; }
}