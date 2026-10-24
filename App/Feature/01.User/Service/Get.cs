using App.Feature.Domain;

namespace App.Feature;

public partial class UserService
{
    public async Task<User?> Get(string username, CancellationToken cancellation)
    {
        var user = await db.Set<User>().SingleOrDefaultAsync(x => x.Username == username,cancellation);
        return user;
    }
}