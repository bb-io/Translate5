using Apps.Translate5.Models.Dtos;

namespace Apps.Translate5.Models.Response.Users;

public record AllUsersResponse(IEnumerable<UserDto> Users);