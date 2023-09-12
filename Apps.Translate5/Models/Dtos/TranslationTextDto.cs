using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Translate5.Models.Dtos;

public class TranslationTextDto
{
    [Display("Target")]
    [JsonProperty("target")]
    public string Target { get; set; }

    [Display("Match rate")]
    [JsonProperty("matchrate")]
    public int Matchrate { get; set; }

    [Display("Source")]
    [JsonProperty("source")]
    public string Source { get; set; }

    [Display("Language resource ID")]
    [JsonProperty("languageResourceid")]
    public string LanguageResourceid { get; set; }

    [Display("Language resource type")]
    [JsonProperty("languageResourceType")]
    public string LanguageResourceType { get; set; }

    [Display("State")]
    [JsonProperty("state")]
    public string State { get; set; }

    [Display("Meta data")]
    [JsonProperty("metaData")]
    public List<MetaData> MetaData { get; set; }
}

public class MetaData
{
    [Display("Name")]
    [JsonProperty("name")]
    public string Name { get; set; }

    [Display("Value")]
    [JsonProperty("value")]
    public string Value { get; set; }
}