using Bale;
using API.Feature.Domain;

namespace API.Feature;

public record UserStateCommandCompletedCommand(long UserId) : ICommand;

public class UserStateCommandCompletedCommandHandler(AppDbContext db) : ICommandHandler<UserStateCommandCompletedCommand>
{
    public async Task ExecuteAsync(UserStateCommandCompletedCommand command, CancellationToken cancellation)
    {
        var userState = await db.Set<UserState>().OrderByDescending(x => x.Id)
            .FirstOrDefaultAsync(x => x.UserId == command.UserId, cancellation);
        if (userState == null || userState.Step == Step.Completed) return;

        userState = new UserState(command.UserId, userState.Flow, Step.Completed,null);
        db.Set<UserState>().Add(userState);
        await db.SaveChangesAsync(cancellation);
    }
}