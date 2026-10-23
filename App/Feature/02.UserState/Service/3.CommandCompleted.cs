using Bale;
using App.Feature.Domain;

namespace App.Feature;

public record UserStateCompleted(long UserId);
public partial class UserStateService
{
    public async Task ExecuteAsync(UserStateCompleted command, CancellationToken cancellation)
    {
        var userState = await new UserStateGet(command.UserId,null).ExecuteAsync(cancellation);
        if (userState == null || userState.Step == Step.Completed) return;

        userState = new UserState(command.UserId, userState.Flow, Step.Completed,null,null);
        db.Set<UserState>().Add(userState);
        await db.SaveChangesAsync(cancellation);
    }
}