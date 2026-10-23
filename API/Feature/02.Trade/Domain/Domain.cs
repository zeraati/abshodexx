namespace API.Feature.Domain;

public partial class Trade
{
    public Trade(long userId, Side side, int geram, decimal price)
    {
        UserId = userId;
        Side = side;
        Geram = geram;
        Price = price;
    }
}