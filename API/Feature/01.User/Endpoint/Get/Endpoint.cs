using API.Feature.Domain;
using Microsoft.AspNetCore.Authorization;

namespace API.Feature.Endpoint;

public class UserGet(AppDbContext db) : FastEndpoint<UserGetRequest, UserGetResponse>
{
    [AllowAnonymous]
    public override void Configure()
    {
        Get("user");
    }

    public override async Task HandleAsync(UserGetRequest request, CancellationToken cancellation)
    {
        var user = await db.Set<User>().SingleAsync(x => x.Id == request.Id,cancellation);
        Response = new UserGetResponse(user);
    }
}
