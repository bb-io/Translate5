using Apps.Translate5.Models.Dtos;

namespace Apps.Translate5.Models.Response.Comments;

public record ListCommentsResponse(IEnumerable<CommentDto> Comments);