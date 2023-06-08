﻿using Apps.Translate5.Dtos;
using Apps.Translate5.Webhooks.Payload;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json;
using System.Text.Json;

namespace Apps.Translate5.Webhooks
{
    [WebhookList]
    public class WebhookList 
    {

        [Webhook("On task import finished", Description = "On task import finished")]
        public async Task<WebhookResponse<TaskImportPayload>> TaskImportFinished(WebhookRequest webhookRequest)
        {
            var data = JsonConvert.DeserializeObject<TaskImportPayload>(webhookRequest.Body.ToString());
            if(data is null)
            {
                throw new InvalidCastException(nameof(webhookRequest.Body));
            }
            return new WebhookResponse<TaskImportPayload>
            {
                HttpResponseMessage = null,
                Result = data
            };
        }

        [Webhook("On tasks finished", Description = "On tasks finished")]
        public async Task<WebhookResponse<TaskImportPayload>> TaskImportFinishedS(WebhookRequest webhookRequest)
        {
            var data = JsonConvert.DeserializeObject<TaskImportPayload>(webhookRequest.Body.ToString());
            if (data is null)
            {
                throw new InvalidCastException(nameof(webhookRequest.Body));
            }
            return new WebhookResponse<TaskImportPayload>
            {
                HttpResponseMessage = null,
                Result = data
            };
        }
    }
}
