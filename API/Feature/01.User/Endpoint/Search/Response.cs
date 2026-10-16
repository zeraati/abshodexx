using API.Feature.Domain;

namespace API.Feature.Endpoint;

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
