using App.Feature.Domain;

namespace App.Feature;

public record UserCreateCommand(string Name, string Username, string? Mobile) : ICommand<User>;

public class UserCreateHandler(IServiceScopeFactory factory) : ICommandHandler<UserCreateCommand, User>
{
    public async Task<User> ExecuteAsync(UserCreateCommand command, CancellationToken cancellation)
    {
        await using var scope = factory.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

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