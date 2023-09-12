using Apps.Translate5.Models.Request.Segments;

namespace Apps.Translate5.Models.Request.Comments;

public class CreateCommentInput : SegmentRequest
{
    public string Comment { get; set; }
}