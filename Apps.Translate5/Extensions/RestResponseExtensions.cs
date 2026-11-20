using System.Net.Http.Headers;
using RestSharp;

namespace Apps.Translate5.Extensions;

public static class RestResponseExtensions
{
    public static string GetFilenameFromDisposition(this RestResponse response, string fallback = "default_filename")
    {
        var fileNameHeader = response.ContentHeaders?.FirstOrDefault(x =>
            x.Name != null && x.Name.Equals("Content-Disposition", StringComparison.OrdinalIgnoreCase));

        if (fileNameHeader == null || !ContentDispositionHeaderValue.TryParse(fileNameHeader.Value?.ToString(), out var disposition)) 
            return fallback;
        
        string fileName = disposition.FileNameStar ?? disposition.FileName ?? fallback;
        fileName = fileName.Trim('\"');

        return !string.IsNullOrEmpty(fileName) ? Uri.UnescapeDataString(fileName) : fallback;
    }
}