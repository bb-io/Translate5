using Apps.Translate5.Models.Request.Tasks;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Translate5.Models.Request.Segments;

public class SegmentRequest : TaskRequest
{
    [Display("Segment ID")]
    public string SegmentId { get; set; }
}