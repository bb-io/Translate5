namespace Apps.Translate5.Models.Request.Comments;

public class CreateCommentRequest
{
    public string SegmentId { get; set; }
    public string Comment { get; set; }

    public CreateCommentRequest(CreateCommentInput input)
    {
        SegmentId = input.SegmentId;
        Comment = input.Comment;
    }
}