using Apps.Translate5.DataSourceHandlers;
using Apps.Translate5.DataSourceHandlers.EnumHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Translate5.Models.Request.Translations;

public class TranslateTextInstantlyRequest
{
    [Display("Source language")]
    [DataSource(typeof(LanguageDataHandler))]
    public string SourceLanguage { get; set; }

    [Display("Target language")]
    [DataSource(typeof(LanguageDataHandler))]
    public string TargetLanguage { get; set; }

    public string Text { get; set; }

    [Display("Language resource")]
    [DataSource(typeof(LanguageResourceDataHandler))]
    public string LanguageResource { get; set; }
}