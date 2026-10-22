using Bale;

namespace API.Feature.Domain;

public partial class UserState
{
    public UserState(long userId, Side side)
    {
        UserId = userId;
        Flow = Flow.None;
        Step = Step.None;
    }

    public void Update(Flow flow, Step step, int? geram)
    {
        Flow = flow;
        Step = step;
        Geram = geram;
    }

    public void Clear()
    {
        Flow = Flow.None;
        Step = Step.None;
    }
}