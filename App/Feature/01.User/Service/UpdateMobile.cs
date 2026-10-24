using App.Feature.Domain;

namespace App.Feature;

public partial class UserService
{
    public async Task<User?> UpdateMobile(string username,string mobile, CancellationToken cancellation)
    {
        var user = await db.Set<User>().SingleAsync(x => x.Username == username,cancellation);
        user.Mobile=mobile;
        return user;
    }
}