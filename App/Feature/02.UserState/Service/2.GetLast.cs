using Bale;
using App.Feature.Domain;

namespace App.Feature;

public record UserStateGetLast(long UserId, Flow? Flow);

public class UserStateGetLastService(AppDbContext db) : IScoped
{
    public async Task<UserState?> ExecuteAsync(UserStateGetLast command, CancellationToken cancellation)
    {
        var userState = await db.Set<UserState>().OrderByDescending(x => x.Id)
            .FirstOrDefaultAsync(x => x.UserId == command.UserId, cancellation);

        if (command.Flow != null) return userState?.Flow == command.Flow ? userState : null;
        return userState;
    }
}