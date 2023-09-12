using Apps.Translate5.Models.Dtos.Simple;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Translate5.Models.Dtos;

public class SegmentDto : SimpleSegmentDto
{
    [Display("Target edit")]

    public string TargetEdit { get; set; }
    
    [Display("Username")]

    public string UserName { get; set; }
}