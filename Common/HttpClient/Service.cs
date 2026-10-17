using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Common.HttpClient;

public partial class HttpClientService(ILogger<HttpClientService> logger, System.Net.Http.HttpClient client)
{
    public Task<HttpClientResponse<TResponse>> Send<TResponse>(HttpClientRequest clientRequest, CancellationToken cancellation)
    {
        return Core<HttpClientResponse<TResponse>>(clientRequest, cancellation);
    }

    public Task<HttpClientResponse> Send(HttpClientRequest clientRequest, CancellationToken cancellation)
    {
        return Core<HttpClientResponse>(clientRequest, cancellation);
    }

    public async Task<Response> Core<Response>(HttpClientRequest clientRequest, CancellationToken cancellation)
        where Response : HttpClientResponse, new()
    {
        using var request = clientRequest.CreateRequest(out var bodyJson);
        LogRequest(request, bodyJson);

        var watch = Stopwatch.StartNew();
        var clientResponse = new Response();

        try
        {
            using var response = await client.SendAsync(request, cancellation);
            watch.Stop();
            await clientResponse.Set(response, cancellation);

            LogResponse(request, response, clientResponse.ResponseContent, watch);
        }

        catch (Exception exception) when (exception is HttpRequestException or TaskCanceledException)
        {
            clientResponse.IsSuccess = false;
            clientResponse.ResponseContent = $"Request failed: {exception.Message}, ResponseContent: {clientResponse.ResponseContent}";
        }

        finally
        {
            watch.Stop();
            clientResponse.DurationMs = watch.ElapsedMilliseconds;
        }

        return clientResponse;
    }
}