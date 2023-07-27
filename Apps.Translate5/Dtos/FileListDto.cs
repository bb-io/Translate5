using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Translate5.Dtos
{
    public class FileListDto
    {
        [JsonProperty("allPretranslatedFiles")]
        public List<AllPretranslatedFile> AllPretranslatedFiles { get; set; }

        [JsonProperty("dateAsOf")]
        public string DateAsOf { get; set; }
    }

    public class AllPretranslatedFile
    {
        [JsonProperty("taskId")]
        public string TaskId { get; set; }

        [JsonProperty("taskName")]
        public string TaskName { get; set; }

        [JsonProperty("sourceLang")]
        public string SourceLang { get; set; }

        [JsonProperty("targetLang")]
        public string TargetLang { get; set; }

        [JsonProperty("downloadUrl")]
        public string DownloadUrl { get; set; }

        [JsonProperty("orderDate")]
        public string OrderDate { get; set; }

        [JsonProperty("removeDate")]
        public string RemoveDate { get; set; }

        [JsonProperty("importProgress")]
        public JToken ImportProgress { get; set; }

        [JsonProperty("errors")]
        public List<object> Errors { get; set; }
    }
}
