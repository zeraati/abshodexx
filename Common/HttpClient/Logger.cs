using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Common.HttpClient;

public partial class HttpClientService
{
    private void LogRequest(HttpRequestMessage request, string? bodyJson)
    {
        var headers = client.DefaultRequestHeaders
            .Concat(request.Headers)
            .Concat(request.Content?.Headers ?? Enumerable.Empty<KeyValuePair<string, IEnumerable<string>>>())
            .Select(header => $"{header.Key}: {string.Join(", ", header.Value)}");

        logger.LogInformation(
            $"""
        Sending HTTP request:
        Method : {request.Method.Method}
        URL    : {request.RequestUri}
        Headers: {string.Join(Environment.NewLine, headers)}
        Body   : {bodyJson ?? "<empty>"}
        """);
    }

    private void LogResponse(HttpRequestMessage request, HttpResponseMessage response, string? responseContent, Stopwatch watch)
    {
        watch.Stop();
        logger.LogInformation(
            $"""
        Received HTTP response:
        Request URL  : {request.RequestUri}
        Response URL : {response.RequestMessage?.RequestUri}
        Status Code  : {(int)response.StatusCode}
        Reason Phrase: {response.ReasonPhrase}
        Headers      : {response.Headers}
        Body         : {responseContent}
        Duration     : {watch.ElapsedMilliseconds} ms
        """);
    }
}




