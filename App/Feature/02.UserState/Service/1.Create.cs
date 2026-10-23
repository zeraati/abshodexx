using App.Feature.Domain;

namespace App.Feature;

public record UserStateCreate(string Username, Flow Flow, Step Step, int? Geram, decimal? Price);

public partial class UserStateService
{
    public async Task<UserState> Create(UserStateCreate command, CancellationToken cancellation)
    {
        var userState = new UserState(command.Username, command.Flow, command.Step, command.Geram,command.Price);
        db.Set<UserState>().Add(userState);
        await db.SaveChangesAsync(cancellation);

        return userState;
    }
}