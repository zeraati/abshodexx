namespace Baha24;

public class GetPriceGold18Response
{
    public string Title { get; set; } = null!;
    public string Symbol { get; set; } = null!;
    public decimal Sell { get; set; }
    public decimal Buy => Sell + 30_000;
    public DateTime LastUpdate { get; set; }
}