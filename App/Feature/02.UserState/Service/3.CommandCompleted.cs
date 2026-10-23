using Bale;
using App.Feature.Domain;

namespace App.Feature;

public record UserStateCommandCompletedCommand(long UserId) : ICommand;

public class UserStateCommandCompletedCommandHandler(AppDbContext db) : IScoped
{
    public async Task ExecuteAsync(UserStateCommandCompletedCommand command, CancellationToken cancellation)
    {
        var userState = await new UserStateGetLast(command.UserId,null).ExecuteAsync(cancellation);
        if (userState == null || userState.Step == Step.Completed) return;

        userState = new UserState(command.UserId, userState.Flow, Step.Completed,null,null);
        db.Set<UserState>().Add(userState);
        await db.SaveChangesAsync(cancellation);
    }
}