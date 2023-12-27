using Apps.Translate5.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Files;


namespace Apps.Translate5.Models.Request.Translations;

public class TranslateFileRequest
{
    [Display("File name")]
    public string? Filename { get; set; }

    public FileReference File { get; set; }

    [Display("Source language")]
    [DataSource(typeof(LanguageDataHandler))]
    public string SourceLanguage { get; set; }

    [Display("Target language")]
    [DataSource(typeof(LanguageDataHandler))]
    public string TargetLanguage { get; set; }
}