using Apps.Translate5.Constants;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.Translate5.Extensions;

public static class RestRequestExtensions
{
    public static RestRequest WithData(this RestRequest request, object dataObj)
    {
        var payload = JsonConvert.SerializeObject(dataObj, JsonConfig.Settings);
        return request.AddParameter("data", payload);
    }
}