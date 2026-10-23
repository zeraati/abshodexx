using Common.HttpClient;
using Microsoft.AspNetCore.Authorization;

namespace App.Feature.Endpoint;

public class UserGet(AppDbContext db, HttpClientService httpClient) : FastEndpoint<UserGetRequest, UserGetResponse>
{
    [AllowAnonymous]
    public override void Configure()
    {
        Get("user");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UserGetRequest request, CancellationToken cancellation)
    {
        //var user = await db.Set<User>().SingleAsync(x => x.Id == request.Id,cancellation);
        //Response = new UserGetResponse(user);

        //var clientRequest = new HttpClientRequest(System.Net.Http.HttpMethod.Get, "https://nerkh.jahankhahan.shop/data/live.json");
        //var response=await httpClient.Send(clientRequest,cancellation);
    }
}