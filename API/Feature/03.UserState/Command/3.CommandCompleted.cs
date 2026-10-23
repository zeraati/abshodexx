using Bale;
using API.Feature.Domain;

namespace API.Feature;

public record UserStateCommandCompletedCommand(long UserId) : ICommand;

public class UserStateCommandCompletedCommandHandler(IServiceScopeFactory factory) : ICommandHandler<UserStateCommandCompletedCommand>
{
    public async Task ExecuteAsync(UserStateCommandCompletedCommand command, CancellationToken cancellation)
    {
        await using var scope = factory.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var userState = await new UserStateGetLastCommand(command.UserId,null).ExecuteAsync(cancellation);
        if (userState == null || userState.Step == Step.Completed) return;

        userState = new UserState(command.UserId, userState.Flow, Step.Completed,null,null);
        db.Set<UserState>().Add(userState);
        await db.SaveChangesAsync(cancellation);
    }
}