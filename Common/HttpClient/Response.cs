using System.Text.Json;

namespace Common.HttpClient;

public class HttpClientResponse
{
    public HttpClientResponse() { }

    public virtual async Task Set(HttpResponseMessage response, CancellationToken cancellation)
    {
        IsSuccess = response.IsSuccessStatusCode;
        StatusCode = (int)response.StatusCode;
        ReasonPhrase = response.ReasonPhrase;
        ResponseContent = await response.Content.ReadAsStringAsync(cancellation);
    }

    public string ResponseContent { get; set; } = null!;
    public bool IsSuccess { get; set; }
    public int? StatusCode { get; private set; }
    public string? ReasonPhrase { get; private set; }
    public long DurationMs { get; set; }
}

public class HttpClientResponse<TResponse> : HttpClientResponse
{
    public override async Task Set(HttpResponseMessage response, CancellationToken cancellation)
    {
        await base.Set(response, cancellation);
        if (IsSuccess == false || string.IsNullOrWhiteSpace(ResponseContent)) return;

        string? provider = typeof(TResponse).FullName?.Contains("Bale.") == true ? "bale": null;
        try { Response = Json.Deserialize<TResponse>(ResponseContent,provider); }
        catch (JsonException exception) { ResponseContent = $"Deserialization failed: {exception.Message}, ResponseContent: {ResponseContent}"; }
    }

    public TResponse? Response { get; private set; }

}


