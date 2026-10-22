using Bale;
using API.Feature.Domain;

namespace API.Feature;

public record UserStateCreateCommand(long UserId, Flow Flow, Step Step, int? Geram) : ICommand<UserState>;

public class UserStateCreateCommandHandler(AppDbContext db) : ICommandHandler<UserStateCreateCommand, UserState>
{
    public async Task<UserState> ExecuteAsync(UserStateCreateCommand command, CancellationToken cancellation)
    {
        var userState = new UserState(command.UserId, command.Flow, command.Step, command.Geram);
        db.Set<UserState>().Add(userState);
        await db.SaveChangesAsync(cancellation);

        return userState;
    }
}