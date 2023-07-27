using Blackbird.Applications.Sdk.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Translate5.Models.Translations.Requests
{
    public class TranslateFileRequest
    {
        public string Filename { get; set; }

        public byte[] File { get; set; }

        [Display("Source language")]
        public string SourceLanguage { get; set; }

        [Display("Target language")]
        public string TargetLanguage { get; set; }
    }
}
