using App.Feature.Domain;

namespace App.Feature.Endpoint;

public class UserGetResponse
{
    public UserGetResponse(User user)
    {
        Id = user.Id;
        Name = user.Name;
    }

    public long Id { get; private set; }
    public string Name { get; private set; }
}
