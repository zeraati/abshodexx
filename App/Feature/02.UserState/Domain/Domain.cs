namespace App.Feature.Domain;

public partial class UserState
{
    public UserState(string username, Flow flow, Step step, int? geram,decimal? price)
    {
        Username = username;
        Flow = flow;
        Step = step;
        Geram = geram;
        Price = price;
    }
}