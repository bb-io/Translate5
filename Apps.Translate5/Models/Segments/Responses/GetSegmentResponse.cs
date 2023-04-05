using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Translate5.Models.Segments.Responses
{
    public class GetSegmentResponse
    {
        public string Id { get; set; }

        public string Source { get; set; }

        public string TargetEdit { get; set; }

        public string UserName { get; set; }
    }
}
