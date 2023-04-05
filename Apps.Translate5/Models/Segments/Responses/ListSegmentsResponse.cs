using Apps.Translate5.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Translate5.Models.Segments.Responses
{
    public class ListSegmentsResponse
    {
        public IEnumerable<SegmentDto> Segments { get; set; }
    }
}
