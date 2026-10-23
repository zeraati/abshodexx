using Bale;
using App.Feature.Domain;

namespace App.Feature;

public record UserStateGetLastCommand(long UserId, Flow? Flow) : ICommand<UserState?>;

public class UserStateGetLastCommandHandler(IServiceScopeFactory factory) : ICommandHandler<UserStateGetLastCommand, UserState?>
{
    public async Task<UserState?> ExecuteAsync(UserStateGetLastCommand command, CancellationToken cancellation)
    {
        await using var scope = factory.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var userState = await db.Set<UserState>().OrderByDescending(x => x.Id)
            .FirstOrDefaultAsync(x => x.UserId == command.UserId, cancellation);

        if (command.Flow != null) return userState?.Flow == command.Flow ? userState : null;
        return userState;
    }
}