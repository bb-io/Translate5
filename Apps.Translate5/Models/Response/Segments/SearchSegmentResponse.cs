using Apps.Translate5.Models.Dtos.Simple;

namespace Apps.Translate5.Models.Response.Segments;

public record SearchSegmentResponse(IEnumerable<SimpleSegmentDto> Segments);