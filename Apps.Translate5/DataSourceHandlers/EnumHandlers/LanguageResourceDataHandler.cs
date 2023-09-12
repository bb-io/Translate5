using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;

namespace Apps.Translate5.DataSourceHandlers.EnumHandlers;

public class LanguageResourceDataHandler : EnumDataHandler
{
    protected override Dictionary<string, string> EnumValues => new()
    {
        { "TermCollection", "TermCollection" },
        { "OpenTM2", "OpenTM2" },
        { "DeepL", "DeepL" },
    };
}