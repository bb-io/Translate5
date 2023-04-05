using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Translate5.Models.Segments.Requests
{
    public class SearchSegmentRequest
    {
        public string TaskId { get; set; }

        public string TaskGuid { get; set; }

        public string SearchField { get; set; }

        public string SearchInField { get; set; } // source/target
    }
}
