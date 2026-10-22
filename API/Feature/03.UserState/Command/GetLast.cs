using Bale;
using API.Feature.Domain;

namespace API.Feature;

public record UserStateGetLastCommand(long UserId,Flow Flow) : ICommand<UserState?>;

public class UserStateGetLastCommandHandler(AppDbContext db) : ICommandHandler<UserStateGetLastCommand, UserState?>
{
    public async Task<UserState?> ExecuteAsync(UserStateGetLastCommand command, CancellationToken cancellation)
    {
        var userState = await db.Set<UserState>().OrderByDescending(x => x.Id)
            .FirstOrDefaultAsync(x => x.UserId == command.UserId && x.Flow == command.Flow, cancellation);

        return userState;
    }
}