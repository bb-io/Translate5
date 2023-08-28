using Apps.Translate5.Dtos;

namespace Apps.Translate5.Models.Comments.Response;

public class ListCommentsResponse
{
    public IEnumerable<CommentDto> Comments { get; set; }
}