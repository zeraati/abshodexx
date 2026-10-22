using Bale;
using API.Feature.Domain;

namespace API.Feature;

public record UserStateStepCompletedCommand(long UserId, Flow Flow) : ICommand<UserState?>;

public class UserStateCreateCommandHandler(AppDbContext db) : ICommandHandler<UserStateStepCompletedCommand, UserState?>
{
    public async Task<UserState?> ExecuteAsync(UserStateStepCompletedCommand command, CancellationToken cancellation)
    {
        var userState = await new UserStateGetLastCommand(command.UserId, command.Flow).ExecuteAsync(cancellation);
        if (userState == null || userState.Geram == null || userState.Step == Step.Completed) return null;

        userState = new UserState(command.UserId, command.Flow, Step.Completed, userState.Geram);
        db.Set<UserState>().Add(userState);
        await db.SaveChangesAsync(cancellation);

        return userState;
    }
}