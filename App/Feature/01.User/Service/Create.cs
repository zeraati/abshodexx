using App.Feature.Domain;

namespace App.Feature;

public record UserCreate(string Name, string Username, string? Mobile);

public partial class UserService
{
    public async Task<User> Create(UserCreate command, CancellationToken cancellation)
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