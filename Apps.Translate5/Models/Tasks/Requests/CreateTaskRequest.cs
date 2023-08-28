using File = Blackbird.Applications.Sdk.Common.Files.File;

namespace Apps.Translate5.Models.Tasks.Requests;

public class CreateTaskRequest
{
    public string TaskName { get; set; }

    public string SourceLanguage { get; set; }

    public string TargetLanguage { get; set; }

    public string FileName { get; set; }  //example: test.txt

    public File File { get; set; }
}