using Apps.Translate5.Api;
using Apps.Translate5.Invocables;
using Apps.Translate5.Models.Dtos;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Translate5.DataSourceHandlers;

public class TaskDataHandler : Translate5Invocable, IAsyncDataSourceHandler
{
    public TaskDataHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var request = new Translate5Request("/editor/task", Method.Get, Creds);
        var items = await Client.Paginate<TaskDto>(request, x => x.Id);

        return items
            .Where(x => context.SearchString is null ||
                        x.TaskName.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Take(40)
            .ToDictionary(x => x.Id, x => x.TaskName);
    }
}