using Blackbird.Applications.Sdk.Common;

namespace Apps.Translate5.Models.Dtos;

public class CommentDto
{
    [Display("Comment ID")] public string Id { get; set; }

    public string Comment { get; set; }

    public string Username { get; set; }
}