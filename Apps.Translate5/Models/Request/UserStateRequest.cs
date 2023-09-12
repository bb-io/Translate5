namespace Apps.Translate5.Models.Request;

public class UserStateRequest
{
    public string UserState { get; set; }

    public UserStateRequest(string state)
    {
        UserState = state;
    }
}