using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Metadata;

namespace Apps.Translate5;

public class Translate5Application : IApplication, ICategoryProvider
{
    public IEnumerable<ApplicationCategory> Categories
    {
        get => [ApplicationCategory.CatAndTms];
        set { }
    }
    
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