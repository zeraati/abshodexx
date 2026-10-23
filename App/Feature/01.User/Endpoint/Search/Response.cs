using App.Feature.Domain;

namespace App.Feature.Endpoint;

public class UserSearchResponse
{
    public UserSearchResponse(User user)
    {
        Id = user.Id;
        Name = user.Name;
    }

    public long Id { get; private set; }
    public string Name { get; private set; }
}
