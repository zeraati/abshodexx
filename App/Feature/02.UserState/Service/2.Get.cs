using App.Feature.Domain;

namespace App.Feature;

public partial class UserStateService
{
    public async Task<UserState?> Get(string username, CancellationToken cancellation)
    {
        var userState = await db.Set<UserState>().OrderByDescending(x => x.Id)
            .FirstOrDefaultAsync(x => x.Username == username, cancellation);

        return userState;
    }
}