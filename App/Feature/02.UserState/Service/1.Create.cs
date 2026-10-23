using Bale;
using App.Feature.Domain;

namespace App.Feature;

public record UserStateCreate(long UserId, Flow Flow, Step Step, int? Geram, decimal? Price);

public class UserStateCreateService(AppDbContext db):IScoped
{
    public async Task<UserState> ExecuteAsync(UserStateCreate command, CancellationToken cancellation)
    {
        var userState = new UserState(command.UserId, command.Flow, command.Step, command.Geram,command.Price);
        db.Set<UserState>().Add(userState);
        await db.SaveChangesAsync(cancellation);

        return userState;
    }
}