using Apps.Translate5.Models.Request.Tasks;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Translate5.Models.Request.Comments;

public class DeleteCommentRequest : TaskRequest
{

    [Display("Comment ID")]
    public string CommentId { get; set; }
}