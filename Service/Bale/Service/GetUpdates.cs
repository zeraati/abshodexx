using Common;
using Common.HttpClient;

namespace Bale;

public partial class BaleService:IScoped
{
    public async Task<Update[]> GetUpdates(long offset, CancellationToken cancellation)
    {
        var request = new HttpClientRequest(HttpMethod.Get, Url.Base, Url.GetUpdates(offset));
        var response = await client.Send<GetUpdatesResponse>(request, cancellation);
        return response?.Response?.Ok == true ? response.Response!.Result ?? [] : [];
    }
}

public class GetUpdatesResponse
{
    public bool Ok { get; set; } = false;
    public Update[]? Result { get; set; }
}
