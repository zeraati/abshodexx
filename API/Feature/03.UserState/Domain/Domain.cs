using Bale;

namespace API.Feature.Domain;

public partial class UserState
{
    public UserState(long userId, Flow flow, Step step, int? geram,decimal? price)
    {
        UserId = userId;
        Flow = flow;
        Step = step;
        Geram = geram;
        Price = price;
    }
}