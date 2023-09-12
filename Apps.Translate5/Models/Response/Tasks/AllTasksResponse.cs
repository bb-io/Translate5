using Apps.Translate5.Models.Dtos;

namespace Apps.Translate5.Models.Response.Tasks;

public record AllTasksResponse(IEnumerable<TaskDto> Tasks);