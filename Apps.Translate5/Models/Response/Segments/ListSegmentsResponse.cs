using Apps.Translate5.Models.Dtos;

namespace Apps.Translate5.Models.Response.Segments;

public record ListSegmentsResponse(IEnumerable<SegmentDto> Segments);