using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Translate5.Models.Segments.Requests
{
    public class ListSegmentsRequest
    {
        public string TaskId { get; set; }

        public int StartIndex { get; set; }

        public int Limit { get; set; }
    }
}
