using App.Feature.Domain;

namespace App.Feature.Endpoint;

public class UserSearch(AppDbContext db) : FastEndpoint<UserSearchRequest, UserSearchResponse[]>
{
    public override void Configure()
    {
        Get("user/search");
    }

    public override async Task HandleAsync(UserSearchRequest request, CancellationToken cancellation)
    {
        var query = db.Set<User>().AsQueryable();
        if (request.Name != null) query = query.Where(x => x.Name.Contains(request.Name));
        var users = await query.Select(x => new UserSearchResponse(x)).ToArrayAsync(cancellation);
        Response = users;
    }
}
