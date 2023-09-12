using Blackbird.Applications.Sdk.Common;

namespace Apps.Translate5.Models.Request.Segments;

public class TranslateSegmentRequest
{
    [Display("Translation")]
    public string TargetEdit { get; set; }
}