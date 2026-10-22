using Bale;

namespace API.Feature.Domain;

public partial class UserState
{
    public UserState(long userId, Flow flow, Step step, int? geram)
    {
        UserId = userId;
        Flow = flow;
        Step = step;
    }
}