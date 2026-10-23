using Common.HttpClient;

namespace Baha24;

public partial class Service
{
    public async Task<GetPriceGold18Response?> GetPriceGold18(CancellationToken cancellation)
    {
        var request = new HttpClientRequest(HttpMethod.Get, Url.Base, Url.Gold18)
        {
            Token = "g9yuZHv9OxH5nGQxBRYD3EZMzjskdyQGHLeyjLEhtZ6gyu4kIkN9vzSxQBBlO0ky"
        };

        var response = await client.Send<GetPriceGold18Response>(request, cancellation);
        return response?.IsSuccess == true ? response.Response : null;
    }
}
