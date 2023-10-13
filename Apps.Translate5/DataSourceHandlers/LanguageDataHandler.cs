using Apps.Translate5.Api;
using Apps.Translate5.Invocables;
using Apps.Translate5.Models.Dtos;
using Apps.Translate5.Models.Response;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Translate5.DataSourceHandlers;

public class LanguageDataHandler : Translate5Invocable, IAsyncDataSourceHandler
{
    public LanguageDataHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var request = new Translate5Request("/editor/language", Method.Get, Creds);
        var items = await Client.ExecuteWithErrorHandling<ResponseWrapper<LanguageDto[]>>(request);

        return items.Rows
            .Where(x => context.SearchString is null ||
                        x.LocalizedName.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .ToDictionary(x => x.Code, x => x.LocalizedName);
    }
}