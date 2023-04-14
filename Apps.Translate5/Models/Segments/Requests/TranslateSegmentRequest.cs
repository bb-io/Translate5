using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Translate5.Models.Segments.Requests
{
    public class TranslateSegmentRequest
    {
        public string SegmentId { get; set; }

        public string TaskId { get; set; }

        public string Translation { get; set; }
    }
}
