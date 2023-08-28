using Blackbird.Applications.Sdk.Common;
using File = Blackbird.Applications.Sdk.Common.Files.File;

namespace Apps.Translate5.Models.Translations.Requests;

public class TranslateFileRequest
{
    public string? Filename { get; set; }

    public File File { get; set; }

    [Display("Source language")]
    public string SourceLanguage { get; set; }

    [Display("Target language")]
    public string TargetLanguage { get; set; }
}