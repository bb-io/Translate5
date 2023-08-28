using Blackbird.Applications.Sdk.Common;
using File = Blackbird.Applications.Sdk.Common.Files.File;

namespace Apps.Translate5.Models.Tasks.Requests;

public class CreateTaskFromZipRequest
{
    public string TaskName { get; set; }

    public string SourceLanguage { get; set; }

    public string TargetLanguage { get; set; }

    public IEnumerable<File> Workfiles { get; set; }

    [Display("Images (visual/image)")]
    public IEnumerable<File>? Images { get; set; }
}
