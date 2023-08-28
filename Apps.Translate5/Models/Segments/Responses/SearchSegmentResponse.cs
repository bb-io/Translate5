using Apps.Translate5.Dtos;

namespace Apps.Translate5.Models.Segments.Responses;

public class SearchSegmentResponse
{
    public IEnumerable<SegmentSearchDto> Segments { get; set; }
}