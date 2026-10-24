using App.Feature.Domain;
using Bale;

namespace App.Feature;

public partial class UserService
{
    public async Task<User?> Get(Update update, CancellationToken cancellation)
    {
        var user = await db.Set<User>().SingleOrDefaultAsync(x => x.Username == update.Username,cancellation);
        return user;
    }
}