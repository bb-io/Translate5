using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Translate5.Models.Tasks.Requests
{
    public class CreateTaskRequest
    {
        public string TaskName { get; set; }

        public string SourceLanguage { get; set; }

        public string TargetLanguage { get; set; }

        public string FileName { get; set; }  //example: test.txt

        public string FileType { get; set; }

        public byte[] File { get; set; }
    }
}
