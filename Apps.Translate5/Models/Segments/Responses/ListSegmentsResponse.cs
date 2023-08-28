using Apps.Translate5.Dtos;

namespace Apps.Translate5.Models.Segments.Responses;

public class ListSegmentsResponse
{
    public IEnumerable<SegmentDto> Segments { get; set; }
}