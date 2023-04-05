using Apps.Translate5.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Translate5.Models.Segments.Responses
{
    public class SearchSegmentResponse
    {
        public IEnumerable<SegmentSearchDto> Segments { get; set; }
    }
}
