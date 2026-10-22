using Bale;
using API.Feature.Domain;

namespace API.Feature;

public record UserStateFlowCompletedCommand(long UserId, Flow Flow, bool Confirmed) : ICommand<UserState?>;

public class UserStateFlowCompletedCommandHandler(AppDbContext db) : ICommandHandler<UserStateFlowCompletedCommand, UserState?>
{
    public async Task<UserState?> ExecuteAsync(UserStateFlowCompletedCommand command, CancellationToken cancellation)
    {
        var userState = await new UserStateGetLastCommand(command.UserId, command.Flow).ExecuteAsync(cancellation);
        if (userState == null || userState.Geram == null || userState.Step == Step.Completed) return null;

        userState = new UserState(command.UserId, command.Flow, Step.Completed, command.Confirmed ? userState.Geram:null);
        db.Set<UserState>().Add(userState);
        await db.SaveChangesAsync(cancellation);

        return userState;
    }
}