using Bale;
using App.Feature.Domain;

namespace App.Feature;

public record UserStateFlowCompleted(long UserId, Flow Flow, bool Confirmed) ;

public partial class UserStateService
{
    public async Task<UserState?> FlowCompleted(UserStateFlowCompleted command, CancellationToken cancellation)
    {
        var userState = await new UserStateGet(command.UserId, command.Flow).ExecuteAsync(cancellation);
        if (userState == null || userState.Geram == null || userState.Step == Step.Completed) return null;

        userState = new UserState(command.UserId, command.Flow, Step.Completed, command.Confirmed ? userState.Geram:null, command.Confirmed ? userState.Price : null);
        db.Set<UserState>().Add(userState);
        await db.SaveChangesAsync(cancellation);

        return userState;
    }
}