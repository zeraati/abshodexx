using System.Net.Http.Headers;
using System.Text;

namespace Common.HttpClient;

public class HttpClientRequest
{
    public HttpClientRequest(HttpMethod method, string baseUrl,string relativeUrl) { Method = method;BaseUrl = baseUrl; RelativeUrl = relativeUrl; }

    public HttpMethod Method { get; private set; }
    public string BaseUrl { get; private set; }
    public string RelativeUrl { get; private set; }
    public object? Body { get; set; }
    public string? Token { get; set; }
    public string? ApiKey { get; set; }

    public HttpRequestMessage CreateRequest(out string? bodyJson)
    {
        if (Uri.TryCreate(new Uri(BaseUrl),RelativeUrl,out var absoluteUri)==false) throw new ArgumentException("Invalid URL");
        var request = new HttpRequestMessage(Method, absoluteUri);

        bodyJson = Body is null ? null : Json.Serialize(Body,"bale");
        if (bodyJson is not null) request.Content = new StringContent(bodyJson, Encoding.UTF8, "application/json");

        if (Token != null) request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
        if (ApiKey != null) request.Headers.Add("XApiKey", ApiKey);

        return request;
    }
}




