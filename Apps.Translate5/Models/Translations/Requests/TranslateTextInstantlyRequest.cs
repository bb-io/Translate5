using Blackbird.Applications.Sdk.Common;

namespace Apps.Translate5.Models.Translations.Requests;

public class TranslateTextInstantlyRequest
{
    [Display("Source language")]
    public string SourceLanguage { get; set; }

    [Display("Target language")]
    public string TargetLanguage { get; set; }

    public string Text { get; set; }

    [Display("Language resource (OpenTM2, DeepL etc.)")]
    public string LanguageResource { get; set; }
}