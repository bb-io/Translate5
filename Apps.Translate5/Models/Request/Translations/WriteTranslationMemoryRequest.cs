using Apps.Translate5.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Newtonsoft.Json;

namespace Apps.Translate5.Models.Request.Translations;

public class WriteTranslationMemoryRequest
{
    [Display("Source language")]
    [JsonProperty("sourceLanguage")]
    [DataSource(typeof(LanguageDataHandler))]
    public string SourceLanguage { get; set; }

    [Display("Target language")]
    [JsonProperty("targetLanguage")]
    [DataSource(typeof(LanguageDataHandler))]
    public string TargetLanguage { get; set; }

    [Display("Source text")]
    [JsonProperty("source")]
    public string Source { get; set; }

    [Display("Target text")]
    [JsonProperty("target")]
    public string Target { get; set; }
}