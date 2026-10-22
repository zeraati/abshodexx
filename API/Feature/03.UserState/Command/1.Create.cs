using Bale;
using API.Feature.Domain;

namespace API.Feature;

public record UserStateCreateCommand(long UserId,Flow Flow) : ICommand<UserState>;

public class UserStateCreateCommandHandler(AppDbContext db) : ICommandHandler<UserStateCreateCommand, UserState>
{
    public async Task<UserState> ExecuteAsync(UserStateCreateCommand command, CancellationToken cancellation)
    {
        var userState = await db.Set<UserState>().OrderByDescending(x=>x.Id).FirstOrDefaultAsync(x => x.UserId==command.UserId, cancellation);
        if (userState == null)
        {
            userState = new UserState(command.UserId, command.Flow, Step.WaitingForEnterGeram);
            db.Set<UserState>().Add(userState);
            await db.SaveChangesAsync(cancellation);
        }

        return userState;
    }
}