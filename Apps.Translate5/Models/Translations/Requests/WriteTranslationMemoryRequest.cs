using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Translate5.Models.Translations.Requests
{
    public class WriteTranslationMemoryRequest
    {
        [Display("Source language")]
        [JsonProperty("sourceLanguage")]
        public string SourceLanguage { get; set; }

        [Display("Target language")]
        [JsonProperty("targetLanguage")]
        public string TargetLanguage { get; set; }

        [Display("Source text")]
        [JsonProperty("source")]
        public string Source { get; set; }

        [Display("Target text")]
        [JsonProperty("target")]
        public string Target { get; set; }
    }
}
