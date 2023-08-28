using Apps.Translate5.Dtos;

namespace Apps.Translate5.Models.Users.Responses;

public class AllUsersResponse
{
    public IEnumerable<UserDto> Users { get; set; }
}