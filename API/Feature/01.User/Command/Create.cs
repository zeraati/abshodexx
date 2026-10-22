using API.Feature.Domain;

namespace API.Feature;

public record UserCreateCommand(string Name, string Username, string? Mobile) : ICommand<User>;

public class UserCreateHandler(AppDbContext db) : ICommandHandler<UserCreateCommand, User>
{
    public async Task<User> ExecuteAsync(UserCreateCommand command, CancellationToken cancellation)
    {
        var user = await db.Set<User>().SingleOrDefaultAsync(x => x.Username == command.Username,cancellation);
        if (user == null)
        {
            user = new User(command.Name, command.Username, command.Mobile);
            db.Set<User>().Add(user);
            await db.SaveChangesAsync(cancellation);
        }

        return user;
    }
}