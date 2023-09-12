using Blackbird.Applications.Sdk.Common;

namespace Apps.Translate5.Models.Dtos.Simple;

public class SimpleSegmentDto
{
    [Display("Segment ID")]
    public string Id { get; set; }

    public string Source { get; set; }
}