namespace API.Feature.Domain;

public partial class GoldPrice
{
    public GoldPrice(string symbol, decimal sell, decimal buy, DateTime lastUpdate)
    {
        Symbol = symbol;
        Sell = sell;
        Buy = buy;
        LastUpdate = lastUpdate;
    }
}
