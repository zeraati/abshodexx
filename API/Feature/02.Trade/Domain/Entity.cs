using Common.Entity;

namespace API.Feature.Domain;

public partial class Trade: BaseEntity
{
    private Trade() { }
    public long UserId { get; set; }

    public Side Side { get; }
    public DateTime? SideAt { get;}

    public int? Lot { get; private set; }
    public DateTime? LotAt { get; private set; }

    public decimal? IndicativePrice { get; private set; } // قیمت اعلامی غیر قطعی
    public DateTime? IndicativePriceAt { get; private set; }

    public decimal? Price { get; private set; }
    public DateTime? PriceAt { get; private set; }
}

public enum Side { Buy, Sell }

public class TradeConfig : IEntityTypeConfiguration<Trade>
{
    public void Configure(EntityTypeBuilder<Trade> builder)
    {
    }
}