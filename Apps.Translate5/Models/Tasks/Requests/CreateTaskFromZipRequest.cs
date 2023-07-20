using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Translate5.Models.Tasks.Requests
{
    public class CreateTaskFromZipRequest
    {
        public string TaskName { get; set; }

        public string SourceLanguage { get; set; }

        public string TargetLanguage { get; set; }

        public IEnumerable<FileData> Workfiles { get; set; }
    }

    public class FileData
    {
        public string Filename { get; set; }

        public byte[] File { get; set; }
    }
}
