using Apps.Translate5.Dtos;

namespace Apps.Translate5.Models.Tasks.Responses;

public class AllTasksResponse
{
    public IEnumerable<TaskDto> Tasks { get; set; }
}