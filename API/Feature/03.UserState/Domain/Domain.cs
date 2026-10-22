using Bale;

namespace API.Feature.Domain;

public partial class UserState
{
    public UserState(long userId,Flow flow,Step step)
    {
        UserId = userId;
        Flow = flow;
        Step = step;
    }

    public void Update(Flow flow, Step step, int? geram)
    {
        Flow = flow;
        Step = step;
        Geram = geram;
    }

    public void Completed()=> Step = Step.Completed;
}