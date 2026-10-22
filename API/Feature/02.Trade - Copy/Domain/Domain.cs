namespace API.Feature.Domain;

public partial class Trade
{
    public Trade(long userId, Side side)
    {
        UserId = userId;
        Side = side;
        SideAt = DateTime.UtcNow;
    }

    public void SetLot(int lot,decimal indicativePrice)
    {
        Lot = lot;
        LotAt = DateTime.UtcNow;
        IndicativePrice = indicativePrice;
    }

    public void SetIndicativePrice(decimal indicativePrice)
    {
        IndicativePrice = indicativePrice;
        IndicativePriceAt = DateTime.UtcNow;
    }

    public void SetPrice(decimal price)
    {
        Price = price;
        PriceAt = DateTime.UtcNow;
    }
}