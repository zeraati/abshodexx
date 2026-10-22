using API.Feature.Domain;

namespace API.Feature;

public record UserStateCreateCommand(long UserId, Side Side) : ICommand<Trade>;

public class TradeCreateCommandHandler(AppDbContext db) : ICommandHandler<TradeCreateCommand, Trade>
{
    public async Task<Trade> ExecuteAsync(TradeCreateCommand command, CancellationToken cancellation)
    {
        var userState = await db.Set<UserState>().OrderByDescending(x=>x.Id).FirstOrDefaultAsync(x => x.Username == command.Username);
        if (userState == null)
        {
            userState = new User(command.Name, command.Username, command.Mobile);
            db.Set<User>().Add(userState);
            await db.SaveChangesAsync(cancellation);
        }

        return userState;
    }
}

UserState