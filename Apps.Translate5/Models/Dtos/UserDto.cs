using Blackbird.Applications.Sdk.Common;

namespace Apps.Translate5.Models.Dtos;

public class UserDto
{
    [Display("User ID")]
    public string Id { get; set; }

    [Display("User GUID")]
    public string UserGuid { get; set; }

    public string Login { get; set; }

    public string Email { get; set; }
}