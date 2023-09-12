﻿using Apps.Translate5.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using File = Blackbird.Applications.Sdk.Common.Files.File;

namespace Apps.Translate5.Models.Request.Tasks;

public class CreateTaskFromZipRequest
{
    [Display("Task name")]
    public string TaskName { get; set; }

    [Display("Source language")]
    [DataSource(typeof(LanguageDataHandler))]
    public string SourceLanguage { get; set; }

    [Display("Target language")]
    [DataSource(typeof(LanguageDataHandler))]
    public string TargetLanguage { get; set; }

    [Display("Work files")]
    public IEnumerable<File> Workfiles { get; set; }

    [Display("Images (visual/image)")]
    public IEnumerable<File>? Images { get; set; }
}
