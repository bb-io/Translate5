using Blackbird.Applications.Sdk.Common;

namespace Apps.Translate5;

public class Translate5Application : IApplication
{
    public string Name
    {
        get => "Translate5";
        set { }
    }

    public T GetInstance<T>()
    {
        throw new NotImplementedException();
    }
}