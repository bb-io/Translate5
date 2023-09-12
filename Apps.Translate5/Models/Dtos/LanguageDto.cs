using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Translate5.Models.Dtos;

public class LanguageDto
{
    [Display("Language ID")]
    public string Id { get; set; }
    
    [JsonProperty("rfc5646")]
    public string Code { get; set; }
    
    [Display("Localized name")]
    public string LocalizedName { get; set; }
}