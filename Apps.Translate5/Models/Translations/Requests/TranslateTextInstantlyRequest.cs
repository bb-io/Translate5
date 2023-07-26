using Blackbird.Applications.Sdk.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Translate5.Models.Translations.Requests
{
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
}
