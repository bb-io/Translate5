using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Translate5.Models.Translations.Response
{
    public class TranslateFileResponse
    {
        public byte[] File { get; set; }

        public string Filename { get; set; }
    }
}
