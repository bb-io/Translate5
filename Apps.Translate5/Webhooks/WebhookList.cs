using Apps.Translate5.Webhooks.Payload;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json;

namespace Apps.Translate5.Webhooks;

[WebhookList]
public class WebhookList
{
    [Webhook("On task import finished", Description = "On task import finished")]
    public Task<WebhookResponse<TaskImportPayload>> OnTaskImportFinished(WebhookRequest webhookRequest)
    {
        var data = JsonConvert.DeserializeObject<TaskImportPayload>(webhookRequest.Body.ToString());

        if (data is null) throw new InvalidCastException(nameof(webhookRequest.Body));

        return Task.FromResult(new WebhookResponse<TaskImportPayload>
        {
            HttpResponseMessage = null,
            Result = data
        });
    }

    [Webhook("On task finished", Description = "On task finished")]
    public Task<WebhookResponse<TaskPayload>> OnTaskFinished(WebhookRequest webhookRequest)
    {
        var data = JsonConvert.DeserializeObject<TaskImportPayload>(webhookRequest.Body.ToString());

        if (data is null) throw new InvalidCastException(nameof(webhookRequest.Body));

        return Task.FromResult(new WebhookResponse<TaskPayload>
        {
            HttpResponseMessage = null,
            Result = data.Task
        });
    }
}