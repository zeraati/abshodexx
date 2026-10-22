using Common.HttpClient;

namespace Bale;
public partial class Service
{
    public async Task<Update[]> GetUpdates(long offset, CancellationToken cancellationToken)
    {
        var request = new HttpClientRequest(HttpMethod.Get, Url.Base, Url.GetUpdates(offset));
        var response = await client.Send<GetUpdatesResponse>(request, cancellationToken);
        return response?.Response?.Ok == true ? response.Response!.Result ?? [] : [];
    }
}
