using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;

namespace Apps.Translate5.DataSourceHandlers.EnumHandlers;

public class SearchInDataHandler : EnumDataHandler
{
    protected override Dictionary<string, string> EnumValues => new()
    {
        { "source", "Source" },
        { "target", "Target" },
    };
}