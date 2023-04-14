using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Translate5.Models.Comments.Requests
{
    public class ListCommentsRequest
    {
        public string SegmentId { get; set; }

        public string TaskId { get; set; }
    }
}
