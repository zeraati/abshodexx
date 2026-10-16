using System.Net.Http.Headers;
using System.Text;

namespace Common.HttpClient;

public class HttpClientRequest
{
    public HttpClientRequest(HttpMethod method, string url) { Method = method; Url = url; }

    public HttpMethod Method { get; private set; }
    public string Url { get; private set; }
    public object? Body { get; set; }
    public string? Token { get; set; }
    public string? ApiKey { get; set; }

    public HttpRequestMessage CreateRequest(out string? bodyJson)
    {
        bodyJson = Body is null ? null : Util.Json.Serialize(Body);

        var request = new HttpRequestMessage(Method, Url);
        if (bodyJson is not null) request.Content = new StringContent(bodyJson, Encoding.UTF8, "application/json");

        if (Token != null) request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
        if (ApiKey != null) request.Headers.Add("XApiKey", ApiKey);

        return request;
    }
}




