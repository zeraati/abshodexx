using Bale;
using App.Feature.Domain;

namespace App.Feature;

public partial class UserService
{
    public async Task<User> Create(Update update, CancellationToken cancellation)
    {
        var user = await db.Set<User>().SingleOrDefaultAsync(x => x.Username == update.Username,cancellation);
        if (user == null)
        {
            var name = update.EffectiveMessage?.From?.Name;
            var mobile = update.EffectiveMessage?.Contact?.PhoneNumber;

            user = new User(name, update.Username!,mobile);
            db.Set<User>().Add(user);
            await db.SaveChangesAsync(cancellation);
        }

        return user;
    }
}